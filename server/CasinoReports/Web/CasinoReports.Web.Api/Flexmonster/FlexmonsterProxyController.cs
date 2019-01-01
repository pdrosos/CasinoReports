namespace Flexmonster.Accelerator.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using Flexmonster.Accelerator.Models;
    using Flexmonster.Accelerator.Utils;

    using Microsoft.AnalysisServices;
    using Microsoft.AnalysisServices.AdomdClient;
    using Microsoft.AspNetCore.Mvc;

    public class FlexmonsterProxyController : ControllerBase
    {
        private const string ErrorComponentVersionCompatibility = "Your current version of Flexmonster Pivot Table & Charts is not compatible with Flexmonster Accelerator.\n\nPlease update the component to the minimum required version: {0}";
        private const string ErrorAcceleratorVersionCompatibility = "Your current version of Flexmonster Accelerator is not compatible with Flexmonster Pivot Table & Charts.\n\nPlease update the Accelerator to the minimum required version: {0}";
        private const string ErrorRequestSignature = "Request signature is not trusted or invalid.";
        private const string ErrorCubeNotFound = "The '{0}' cube either does not exist or has not been processed.";
        private const string ErrorSessionExpired = "ERROR_SESSION_EXPIRED";

        private static readonly ILogger Log = LoggerLocator.GetLogger();
        private static readonly ICacheManager CacheManager = (ICacheManager)Accelerator.Utils.CacheManager.GetCache();
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>>> CachedAllMembers = new ConcurrentDictionary<string, ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>>>();
        private static readonly ConcurrentDictionary<string, int> DataChunkSize = new ConcurrentDictionary<string, int>();
        private static readonly ConcurrentDictionary<string, int> DataChunkFromIndex = new ConcurrentDictionary<string, int>();
        private static readonly ConcurrentDictionary<string, int> DataChunkTotalRows = new ConcurrentDictionary<string, int>();

        private static readonly string VersionNumber = "2.6.10";
        private static readonly string ComponentMinVersion = "2.417";
        private static readonly int MembersChunkSize = 10000;
        private static readonly int Datachunksize = 500;
        private static readonly string ConnectionString = string.Empty;

        private readonly Dictionary<string, string> connectionStrings = new Dictionary<string, string>();

        protected virtual ICacheManager Cache => CacheManager;

        [HttpPost]
        public HttpResponseMessage Handshaking([FromBody] HandshakingArgs args)
        {
            if (args.componentVersionLabel != null)
            {
                args.componentVersion = args.componentVersionLabel;
            }

            Log.Trace("Handshaking... " + args.componentVersion);
            HandshakingResponse handshakingResponse = new HandshakingResponse();
            if (this.CompareVersions(args.componentVersion, ComponentMinVersion) == -1)
            {
                handshakingResponse.error = new ErrorVO()
                {
                    message = $"Your current version of Flexmonster Pivot Table & Charts is not compatible with Flexmonster Accelerator.\n\nPlease update the component to the minimum required version: {ComponentMinVersion}",
                };

                Log.Warn(handshakingResponse.error.message);
            }
            else if (this.CompareVersions(VersionNumber, args.acceleratorMinVersion) == -1)
            {
                handshakingResponse.error = new ErrorVO()
                {
                    message = $"Your current version of Flexmonster Accelerator is not compatible with Flexmonster Pivot Table & Charts.\n\nPlease update the Accelerator to the minimum required version: {args.componentVersion}",
                };

                Log.Warn(handshakingResponse.error.message);
            }

            handshakingResponse.version = VersionNumber;
            handshakingResponse.cacheEnabled = Accelerator.Utils.CacheManager.Enabled;

            return this.ComposeResponse(handshakingResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverDimensions([FromBody] DiscoverArgs args)
        {
            Log.Trace("Loading dimensions...");
            this.OnRequest(args);
            DiscoverDimensionsResponse dimensionsResponse = new DiscoverDimensionsResponse();
            try
            {
                if (!RequestValidator.IsValidRequest(args))
                {
                    dimensionsResponse.error = new ErrorVO()
                    {
                        message = "Request signature is not trusted or invalid.",
                    };

                    Log.Warn(dimensionsResponse.error.message);

                    return this.ComposeResponse(dimensionsResponse.toJSON());
                }

                args.method = nameof(this.DiscoverDimensions);
                string catalog = args.catalog;
                string cubeUniqueName = args.cubeUniqueName;
                if (args.sessionId <= 0L)
                {
                    args.sessionId = this.GetSessionId(catalog, args.credentials, cubeUniqueName, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles);
                }

                if (this.Cache.TryGet(args, out var foundItem))
                {
                    return this.ComposeResponse(foundItem);
                }

                DateTime now = DateTime.Now;
                List<DimensionVO> dimensionVoList = new List<DimensionVO>();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    CubeDef cube = connection.Cubes[cubeUniqueName];
                    if (cube != null)
                    {
                        foreach (Microsoft.AnalysisServices.AdomdClient.Dimension dimension in cube.Dimensions)
                        {
                            dimensionVoList.Add(DimensionVO.FromItem(dimension));
                        }
                    }
                    else
                    {
                        dimensionsResponse.error = new ErrorVO()
                        {
                            message = $"The '{cubeUniqueName}' cube either does not exist or has not been processed.",
                        };

                        Log.Trace("Error! " + dimensionsResponse.error.message);
                    }

                    connection.Close();
                }

                dimensionsResponse.data = dimensionVoList.ToArray();
                dimensionsResponse.sessionId = args.sessionId;
                this.Cache.Add(args, dimensionsResponse.toJSON());
                Log.Trace("Dimensions: " + dimensionVoList.Count + " loaded [" + this.TimeDiff(now) + " ms]");
            }
            catch (Exception ex)
            {
                dimensionsResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(dimensionsResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverHierarchies([FromBody] DiscoverArgs args)
        {
            Log.Trace("Loading hierarchies...");
            this.OnRequest(args);
            DiscoverHierarchiesResponse hierarchiesResponse = new DiscoverHierarchiesResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                hierarchiesResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };

                Log.Warn(hierarchiesResponse.error.message);

                return this.ComposeResponse(hierarchiesResponse.toJSON());
            }

            args.method = nameof(this.DiscoverHierarchies);
            string catalog = args.catalog;
            string cubeUniqueName = args.cubeUniqueName;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            try
            {
                DateTime now = DateTime.Now;
                List<HierarchyVO> hierarchyVoList = new List<HierarchyVO>();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    CubeDef cube = connection.Cubes[cubeUniqueName];
                    if (cube == null)
                    {
                        hierarchiesResponse.error = new ErrorVO()
                        {
                            message = $"The '{cubeUniqueName}' cube either does not exist or has not been processed.",
                        };

                        Log.Error(hierarchiesResponse.error.message);
                    }
                    else if (this.IsSessionExpired(args.sessionId, cube))
                    {
                        hierarchiesResponse.error = new ErrorVO()
                        {
                            message = "ERROR_SESSION_EXPIRED",
                        };

                        Log.Error(hierarchiesResponse.error.message);
                    }
                    else
                    {
                        foreach (Microsoft.AnalysisServices.AdomdClient.Dimension dimension in cube.Dimensions)
                        {
                            foreach (Microsoft.AnalysisServices.AdomdClient.Hierarchy hierarchy in dimension.Hierarchies)
                            {
                                hierarchyVoList.Add(HierarchyVO.FromItem(hierarchy));
                            }
                        }
                    }

                    connection.Close();
                }

                hierarchiesResponse.data = hierarchyVoList.ToArray();
                this.Cache.Add(args, hierarchiesResponse.toJSON());
                Log.Trace("Hierarchies: " + hierarchyVoList.Count + " loaded [" + this.TimeDiff(now) + " ms]");
            }
            catch (Exception ex)
            {
                hierarchiesResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(hierarchiesResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverLevels([FromBody] DiscoverArgs args)
        {
            Log.Trace("Loading levels...");
            this.OnRequest(args);
            DiscoverLevelsResponse discoverLevelsResponse = new DiscoverLevelsResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                discoverLevelsResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };
                Log.Warn(discoverLevelsResponse.error.message);

                return this.ComposeResponse(discoverLevelsResponse.toJSON());
            }

            args.method = nameof(this.DiscoverLevels);
            string catalog = args.catalog;
            string cubeUniqueName = args.cubeUniqueName;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            try
            {
                DateTime now = DateTime.Now;
                List<LevelVO> levelVoList = new List<LevelVO>();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    CubeDef cube = connection.Cubes[cubeUniqueName];
                    if (cube == null)
                    {
                        discoverLevelsResponse.error = new ErrorVO()
                        {
                            message = $"The '{cubeUniqueName}' cube either does not exist or has not been processed.",
                        };

                        Log.Error(discoverLevelsResponse.error.message);
                    }
                    else if (this.IsSessionExpired(args.sessionId, cube))
                    {
                        discoverLevelsResponse.error = new ErrorVO()
                        {
                            message = "ERROR_SESSION_EXPIRED",
                        };

                        Log.Error(discoverLevelsResponse.error.message);
                    }
                    else
                    {
                        foreach (Microsoft.AnalysisServices.AdomdClient.Dimension dimension in cube.Dimensions)
                        {
                            foreach (Microsoft.AnalysisServices.AdomdClient.Hierarchy hierarchy in dimension.Hierarchies)
                            {
                                foreach (Microsoft.AnalysisServices.AdomdClient.Level level in hierarchy.Levels)
                                {
                                    levelVoList.Add(LevelVO.FromItem(level));
                                }
                            }
                        }
                    }

                    connection.Close();
                }

                discoverLevelsResponse.data = levelVoList.ToArray();
                this.Cache.Add(args, discoverLevelsResponse.toJSON());
                Log.Trace("Levels: " + levelVoList.Count + " loaded [" + this.TimeDiff(now) + " ms]");
            }
            catch (Exception ex)
            {
                discoverLevelsResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(discoverLevelsResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverMeasures([FromBody] DiscoverArgs args)
        {
            Log.Trace("Loading measures...");
            this.OnRequest(args);
            DiscoverMeasuresResponse measuresResponse = new DiscoverMeasuresResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                measuresResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };

                Log.Warn(measuresResponse.error.message);

                return this.ComposeResponse(measuresResponse.toJSON());
            }

            args.method = nameof(this.DiscoverMeasures);
            string catalog = args.catalog;
            string cubeUniqueName = args.cubeUniqueName;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            try
            {
                DateTime now = DateTime.Now;
                List<MeasureVO> measureVoList = new List<MeasureVO>();
                Dictionary<string, string> groupCaptions = new Dictionary<string, string>();
                Server server = new Server();
                server.Connect(ConnectionString);
                if (!string.IsNullOrEmpty(args.localeIdentifier))
                {
                    int language = int.Parse(args.localeIdentifier);
                    foreach (Database database in server.Databases)
                    {
                        if (database.Name == catalog)
                        {
                            foreach (Cube cube in database.Cubes)
                            {
                                if (cube.Name == cubeUniqueName)
                                {
                                    foreach (MeasureGroup measureGroup in cube.MeasureGroups)
                                    {
                                        if (measureGroup.Translations.Contains(language))
                                        {
                                            Translation byLanguage = measureGroup.Translations.GetByLanguage(language);
                                            if (byLanguage != null)
                                            {
                                                groupCaptions[measureGroup.Name] = byLanguage.Caption;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                server.Disconnect();
                server.Dispose();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    CubeDef cube = connection.Cubes[cubeUniqueName];
                    if (cube == null)
                    {
                        measuresResponse.error = new ErrorVO()
                        {
                            message = $"The '{cubeUniqueName}' cube either does not exist or has not been processed.",
                        };

                        Log.Error(measuresResponse.error.message);
                    }
                    else if (this.IsSessionExpired(args.sessionId, cube))
                    {
                        measuresResponse.error = new ErrorVO()
                        {
                            message = "ERROR_SESSION_EXPIRED",
                        };

                        Log.Error(measuresResponse.error.message);
                    }
                    else
                    {
                        foreach (Microsoft.AnalysisServices.AdomdClient.Measure measure in cube.Measures)
                        {
                            measureVoList.Add(MeasureVO.FromItem(measure, groupCaptions));
                        }
                    }

                    connection.Close();
                }

                measuresResponse.data = measureVoList.ToArray();
                this.Cache.Add(args, measuresResponse.toJSON());
                Log.Trace("Measures: " + measureVoList.Count + " loaded [" + this.TimeDiff(now) + " ms]");
            }
            catch (Exception ex)
            {
                measuresResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(measuresResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverKpis([FromBody] DiscoverArgs args)
        {
            Log.Trace("Loading KPIs...");
            this.OnRequest(args);
            DiscoverKpisResponse discoverKpisResponse = new DiscoverKpisResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                discoverKpisResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };
                Log.Warn(discoverKpisResponse.error.message);

                return this.ComposeResponse(discoverKpisResponse.toJSON());
            }

            args.method = nameof(this.DiscoverKpis);
            string catalog = args.catalog;
            string cubeUniqueName = args.cubeUniqueName;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            try
            {
                DateTime now = DateTime.Now;
                List<KpiVO> kpiVoList = new List<KpiVO>();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    CubeDef cube = connection.Cubes[cubeUniqueName];
                    if (cube == null)
                    {
                        discoverKpisResponse.error = new ErrorVO()
                        {
                            message = $"The '{cubeUniqueName}' cube either does not exist or has not been processed.",
                        };

                        Log.Error(discoverKpisResponse.error.message);
                    }
                    else if (this.IsSessionExpired(args.sessionId, cube))
                    {
                        discoverKpisResponse.error = new ErrorVO()
                        {
                            message = "ERROR_SESSION_EXPIRED",
                        };

                        Log.Error(discoverKpisResponse.error.message);
                    }
                    else
                    {
                        foreach (Microsoft.AnalysisServices.AdomdClient.Kpi kpi in cube.Kpis)
                        {
                            kpiVoList.Add(KpiVO.FromItem(kpi));
                        }
                    }

                    connection.Close();
                }

                discoverKpisResponse.data = kpiVoList.ToArray();
                this.Cache.Add(args, discoverKpisResponse.toJSON());
                Log.Trace("KPIs: " + kpiVoList.Count + " loaded [" + this.TimeDiff(now) + " ms]");
            }
            catch (Exception ex)
            {
                discoverKpisResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(discoverKpisResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DiscoverMembers([FromBody] DiscoverMembersArgs args)
        {
            Log.Trace("Loading members..." + args.hierarchyUniqueName + " - " + args.chunkIndex);
            this.OnRequest(args);
            DiscoverMembersResponse discoverMembersResponse = new DiscoverMembersResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                discoverMembersResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };

                Log.Warn(discoverMembersResponse.error.message);

                return this.ComposeResponse(discoverMembersResponse.toJSON());
            }

            string catalog = args.catalog;
            string str1 = args.subquery == string.Empty ? "[" + args.cubeUniqueName + "]" : "(" + args.subquery + ")";
            string hierarchyUniqueName = args.hierarchyUniqueName;
            string memberUniqueName = args.memberUniqueName;
            string properties = args.properties;
            string searchPhrase = args.searchPhrase.Replace("'", "''").Replace("\"", "\"\"");
            string ascendants = args.ascendants;
            string siblings = args.siblings;
            string activeLevel = args.activeLevel;
            bool hasAllMember = args.hasAllMember;
            string allMemberName = args.allMemberName;
            int fromIndex = args.chunkIndex * MembersChunkSize;
            bool loadAll = args.loadAll;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            if (this.IsSessionExpired(args.sessionId, this.GetSessionId(catalog, args.credentials, args.cubeUniqueName, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles)))
            {
                discoverMembersResponse.error = new ErrorVO()
                {
                    message = "ERROR_SESSION_EXPIRED",
                };

                Log.Error(discoverMembersResponse.error.message);

                return this.ComposeResponse(discoverMembersResponse.toJSON());
            }

            try
            {
                DateTime now = DateTime.Now;
                MembersResponseVO membersResponseVo = new MembersResponseVO();
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    if (connection.Cubes[args.cubeUniqueName] == null)
                    {
                        discoverMembersResponse.error = new ErrorVO()
                        {
                            message = $"The '{str1}' cube either does not exist or has not been processed.",
                        };

                        Log.Error(discoverMembersResponse.error.message);
                    }
                    else
                    {
                        if (args.chunkIndex == 0)
                        {
                            membersResponseVo.all = this.CountMembers(args, true, hasAllMember, connection);
                            if (!loadAll)
                            {
                                loadAll = membersResponseVo.all <= MembersChunkSize;
                            }
                        }

                        string str2 = string.IsNullOrEmpty(searchPhrase) ? (string.IsNullOrEmpty(memberUniqueName) ? (string.IsNullOrEmpty(ascendants) ? (string.IsNullOrEmpty(siblings) ? MDXGenerator.Members(str1, hierarchyUniqueName, memberUniqueName, activeLevel, properties, hasAllMember, loadAll, fromIndex, MembersChunkSize) : MDXGenerator.Siblings(str1, siblings, properties, fromIndex, MembersChunkSize, hierarchyUniqueName)) : MDXGenerator.Ascendants(str1, ascendants, properties, fromIndex, MembersChunkSize, hierarchyUniqueName)) : MDXGenerator.Children(str1, memberUniqueName, properties, fromIndex, MembersChunkSize, hierarchyUniqueName)) : MDXGenerator.Search(str1, searchPhrase, hierarchyUniqueName, allMemberName, fromIndex, MembersChunkSize);
                        Log.Trace(str2);
                        using (AdomdCommand adomdCommand = new AdomdCommand(str2, connection))
                        {
                            ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>> membersFromCache = this.GetMembersFromCache(catalog, str1, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles, (double)args.sessionId);
                            ConcurrentDictionary<string, MemberVO> allMembers;
                            if (membersFromCache != null && membersFromCache.TryGetValue(hierarchyUniqueName, out allMembers))
                            {
                                fromIndex = allMembers.Count;
                            }
                            else
                            {
                                allMembers = new ConcurrentDictionary<string, MemberVO>();
                            }

                            membersResponseVo.members = this.GetMembers(adomdCommand.ExecuteCellSet(), fromIndex, hasAllMember, allMemberName, allMembers);
                            if (args.chunkIndex == 0)
                            {
                                membersResponseVo.count = this.CountMembers(args, false, hasAllMember, connection);
                            }

                            if (searchPhrase != null)
                            {
                                if (searchPhrase != string.Empty)
                                {
                                    goto label_24;
                                }
                            }

                            this.AddMembersToCache(catalog, str1, hierarchyUniqueName, membersResponseVo.members, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles, (double)args.sessionId);
                        }

                    label_24:
                        connection.Close();
                        discoverMembersResponse.data = membersResponseVo;
                    }

                    this.Cache.Add(args, discoverMembersResponse.toJSON());
                    Log.Trace("Members of " + hierarchyUniqueName + ": " + membersResponseVo.members.Length + " loaded [" + this.TimeDiff(now) + " ms]");
                }
            }
            catch (Exception ex)
            {
                discoverMembersResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(discoverMembersResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage Execute([FromBody] ExecuteArgs args)
        {
            Log.Trace(nameof(this.Execute));
            this.OnRequest(args);
            DataResponse dataResponse = new DataResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                dataResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };

                Log.Warn(dataResponse.error.message);

                return this.ComposeResponse(dataResponse.toJSON());
            }

            string catalog = args.catalog;
            string cubeOrSubquery = string.IsNullOrEmpty(args.subquery) ? "[" + args.cubeUniqueName + "]" : "(" + args.subquery + ")";
            string queryString = args.queryString;
            string[] columns = args.columns;
            string[] rows = args.rows;
            string[] measures = args.measures;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            if (this.IsSessionExpired(args.sessionId, this.GetSessionId(catalog, args.credentials, args.cubeUniqueName, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles)))
            {
                dataResponse.error = new ErrorVO()
                {
                    message = "ERROR_SESSION_EXPIRED",
                };

                Log.Error(dataResponse.error.message);

                return this.ComposeResponse(dataResponse.toJSON());
            }

            try
            {
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    connection.Open();
                    string key1 = catalog + queryString + args.credentials + args.sessionId + args.customData + args.localeIdentifier + args.effectiveUserName + args.roles + args.formatted.ToString();
                    int orAdd = DataChunkTotalRows.GetOrAdd(key1, x =>
                    {
                        if (queryString.IndexOf("_ROWS_TOKEN_START_") == -1)
                        {
                            return 1;
                        }

                        return this.CountExecuteRows(cubeOrSubquery, measures, queryString, connection);
                    });
                    string key2 = key1 + (args.chunkIndex - 1);
                    string index = key1 + args.chunkIndex;
                    if (!DataChunkSize.TryGetValue(key2, out var num1))
                    {
                        num1 = Datachunksize;
                    }

                    if (!DataChunkFromIndex.TryGetValue(key2, out var num2))
                    {
                        num2 = 0;
                    }

                    string commandText = queryString.Replace("_ROWS_TOKEN_START_", "Subset(").Replace("_ROWS_TOKEN_END_", ", " + num2 + ", " + num1 + ")");
                    Log.Trace("chunk: " + args.chunkIndex);
                    Log.Trace("query: " + commandText);
                    DateTime now = DateTime.Now;
                    using (AdomdCommand adomdCommand = new AdomdCommand(commandText, connection))
                    {
                        CellSet input = adomdCommand.ExecuteCellSet();
                        dataResponse.data = DataSetVO.FromItem(input, rows, columns, measures, this.GetMembersFromCache(catalog, cubeOrSubquery, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles, (double)args.sessionId), args.formatted);
                        if (dataResponse.data.cellData.Length != 0)
                        {
                            dataResponse.data.isComplete = false;
                            int num3 = this.TimeDiff(now);
                            Log.Trace("duration: " + num3);
                            if (args.chunkIndex == 0)
                            {
                                DataChunkFromIndex[index] = num1;
                                num1 = num3 > 0 ? 1000000 / num3 : 1000000;
                                DataChunkSize[index] = num1;
                            }
                            else
                            {
                                DataChunkFromIndex[index] = DataChunkFromIndex[key2] + num1;
                                if (num3 < 2000)
                                {
                                    if (num3 < 500)
                                    {
                                        num1 *= 2;
                                    }
                                    else
                                    {
                                        num1 = (int)(num1 * 1.2);
                                    }
                                }
                                else
                                {
                                    num1 = num3 <= 5000 ? (int)(num1 * 0.9) : (int)(num1 * 0.66);
                                }

                                if (num1 < 50)
                                {
                                    num1 = 50;
                                }

                                DataChunkSize[index] = num1;
                            }

                            dataResponse.data.percent = DataChunkFromIndex[index] / (double)orAdd;
                            Log.Trace("Execute - set chunk size: " + num1 + " " + DataChunkFromIndex[index] + " " + orAdd);
                            Log.Trace("Execute - OK! " + (dataResponse.data.cellData.Length * dataResponse.data.cellData[0].Length) + " cells [" + num3 + " ms]");
                        }
                        else
                        {
                            if (args.chunkIndex == 0)
                            {
                                DataChunkFromIndex[index] = num1;
                            }
                            else
                            {
                                DataChunkFromIndex[index] = DataChunkFromIndex[key2] + num1;
                                DataChunkSize[index] = num1 * 2;
                            }

                            Log.Trace("Execute - empty [" + this.TimeDiff(now) + " ms] " + num1);
                            if (num2 + num1 > orAdd)
                            {
                                dataResponse.data.isComplete = true;
                            }
                        }

                        if (orAdd == 1)
                        {
                            dataResponse.data.isComplete = true;
                        }

                        if (dataResponse.data.isComplete)
                        {
                            int num3;
                            DataChunkTotalRows.TryRemove(key1, out num3);
                        }
                    }

                    connection.Close();
                    this.Cache.Add(args, dataResponse.toJSON());
                }
            }
            catch (Exception ex)
            {
                dataResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };

                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            Log.Trace("Execute end");

            return this.ComposeResponse(dataResponse.toJSON());
        }

        [HttpPost]
        public virtual HttpResponseMessage DrillThrough([FromBody] DrillThroughArgs args)
        {
            Log.Trace("Drill through...");
            this.OnRequest(args);
            DataResponse dataResponse = new DataResponse();
            if (!RequestValidator.IsValidRequest(args))
            {
                dataResponse.error = new ErrorVO()
                {
                    message = "Request signature is not trusted or invalid.",
                };
                Log.Warn(dataResponse.error.message);

                return this.ComposeResponse(dataResponse.toJSON());
            }

            string catalog = args.catalog;
            string fromQuery = string.IsNullOrEmpty(args.subquery) ? "[" + args.cubeUniqueName + "]" : "(" + args.subquery + ")";
            string measureUniqueName = args.measureUniqueName;
            string[] members = args.members;
            if (this.Cache.TryGet(args, out var foundItem))
            {
                return this.ComposeResponse(foundItem);
            }

            if (this.IsSessionExpired(args.sessionId, this.GetSessionId(catalog, args.credentials, args.cubeUniqueName, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles)))
            {
                dataResponse.error = new ErrorVO()
                {
                    message = "ERROR_SESSION_EXPIRED",
                };
                Log.Error(dataResponse.error.message);

                return this.ComposeResponse(dataResponse.toJSON());
            }

            try
            {
                DateTime now = DateTime.Now;
                using (AdomdConnection connection = this.CreateConnection(catalog, args.credentials, args.customData, args.localeIdentifier, args.effectiveUserName, args.roles))
                {
                    string str = MDXGenerator.DrillThrough(fromQuery, measureUniqueName, members);
                    Log.Trace(str);
                    connection.Open();
                    using (AdomdCommand adomdCommand = new AdomdCommand(str, connection))
                    {
                        dataResponse.data = DataSetVO.FromItem(adomdCommand.ExecuteReader());
                        this.Cache.Add(args, dataResponse.toJSON());
                        Log.Trace("Drill through - OK! " + dataResponse.data.cellData.Length + " rows [" + this.TimeDiff(now) + " ms]");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dataResponse.error = new ErrorVO()
                {
                    message = ex.Message,
                };
                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return this.ComposeResponse(dataResponse.toJSON());
        }

        public virtual void OnRequest(BaseArgs args)
        {
        }

        private int CompareVersions(string versionA, string versionB)
        {
            if (versionA == versionB)
            {
                return 0;
            }

            string[] strArray1 = versionA.Split('.');
            string[] strArray2 = versionB.Split('.');
            if (strArray1.Length < strArray2.Length)
            {
                return -1;
            }

            if (strArray1.Length > strArray2.Length)
            {
                return 1;
            }

            for (int index = 0; index < strArray1.Length; ++index)
            {
                int num1 = int.Parse(strArray1[index]);
                int num2 = int.Parse(strArray2[index]);
                if (num1 < num2)
                {
                    return -1;
                }

                if (num1 > num2)
                {
                    return 1;
                }
            }

            return 0;
        }

        private void AddMembersToCache(
          string catalog,
          string cube,
          string hierarchy,
          MemberVO[] allMembers,
          string credentials,
          string customData,
          string localeIdentifier,
          string effectiveUserName,
          string roles,
          double sessionId)
        {
            string key = catalog + cube + credentials + customData + localeIdentifier + effectiveUserName + roles + sessionId;
            ConcurrentDictionary<string, MemberVO> orAdd = CachedAllMembers.GetOrAdd(
                key,
                new ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>>())
                .GetOrAdd(hierarchy, new ConcurrentDictionary<string, MemberVO>());

            foreach (MemberVO allMember in allMembers)
            {
                orAdd.TryAdd(allMember.uniqueName, allMember);
            }
        }

        private ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>> GetMembersFromCache(
          string catalog,
          string cube,
          string credentials,
          string customData,
          string localeIdentifier,
          string effectiveUserName,
          string roles,
          double sessionId)
        {
            string key = catalog + cube + credentials + customData + localeIdentifier + effectiveUserName + roles + sessionId;

            return CachedAllMembers.GetOrAdd(key, new ConcurrentDictionary<string, ConcurrentDictionary<string, MemberVO>>());
        }

        private int CountMembers(
          DiscoverMembersArgs args,
          bool isAll,
          bool hasAllMember,
          AdomdConnection connection)
        {
            string cubeUniqueName = args.subquery == string.Empty ? "[" + args.cubeUniqueName + "]" : "(" + args.subquery + ")";
            string hierarchyUniqueName = args.hierarchyUniqueName;
            string memberUniqueName = args.memberUniqueName;
            string searchPhrase = args.searchPhrase.Replace("'", "''").Replace("\"", "\"\"");
            string allMemberName = args.allMemberName;
            string ascendants = args.ascendants;
            string siblings = args.siblings;
            Log.Trace("Counting members ... " + args.hierarchyUniqueName + " - " + isAll);

            try
            {
                DateTime now = DateTime.Now;
                string str = !isAll ? (string.IsNullOrEmpty(searchPhrase) ? (string.IsNullOrEmpty(memberUniqueName) ? (string.IsNullOrEmpty(ascendants) ? (string.IsNullOrEmpty(siblings) ? MDXGenerator.CountMembers(cubeUniqueName, hierarchyUniqueName, (string)null, hasAllMember) : MDXGenerator.CountSiblings(cubeUniqueName, hierarchyUniqueName, siblings)) : MDXGenerator.CountAscendants(cubeUniqueName, hierarchyUniqueName, ascendants)) : MDXGenerator.CountMembers(cubeUniqueName, hierarchyUniqueName, memberUniqueName, hasAllMember)) : MDXGenerator.CountMembersSearch(cubeUniqueName, searchPhrase, hierarchyUniqueName, allMemberName)) : MDXGenerator.CountAllMembers(cubeUniqueName, hierarchyUniqueName);
                Log.Trace("Hierarachy: " + hierarchyUniqueName);
                Log.Trace("Search phrase: " + searchPhrase);
                Log.Trace("Member: " + memberUniqueName);
                Log.Trace(str);

                using (AdomdCommand adomdCommand = new AdomdCommand(str, connection))
                {
                    using (AdomdDataReader adomdDataReader = adomdCommand.ExecuteReader())
                    {
                        if (adomdDataReader.Read())
                        {
                            Log.Trace("Count members of " + hierarchyUniqueName + ": " + adomdDataReader[0] + " [" + this.TimeDiff(now) + " ms]");

                            return (int)adomdDataReader[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return 0;
        }

        private int CountExecuteRows(
          string cubeUniqueName,
          string[] measures,
          string queryString,
          AdomdConnection connection)
        {
            Log.Trace("Counting rows ...");
            try
            {
                DateTime now = DateTime.Now;
                queryString = MDXGenerator.CountExecuteRows(cubeUniqueName, queryString);
                Log.Trace(queryString);
                using (AdomdCommand adomdCommand = new AdomdCommand(queryString, connection))
                {
                    using (AdomdDataReader adomdDataReader = adomdCommand.ExecuteReader())
                    {
                        if (adomdDataReader.Read())
                        {
                            Log.Trace("Total rows: " + adomdDataReader[0] + " [" + this.TimeDiff(now) + " ms]");

                            return (int)adomdDataReader[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
            }

            return 0;
        }

        private AdomdConnection CreateConnection(
          string catalog,
          string credentials,
          string customData,
          string localeIdentifier,
          string effectiveUserName,
          string roles)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("FlexmonsterProxyController.ConnectionString is not specified.");
            }

            string key = catalog + credentials + customData + localeIdentifier + effectiveUserName + roles;
            if (this.connectionStrings.ContainsKey(key))
            {
                Log.Trace("Connection string: {0}", this.connectionStrings[key]);

                return new AdomdConnection(this.connectionStrings[key]);
            }

            ConnectionStringBuilder connectionStringBuilder = new ConnectionStringBuilder(ConnectionString);
            if (!string.IsNullOrEmpty(catalog))
            {
                connectionStringBuilder.Set("Catalog", catalog);
            }

            if (!string.IsNullOrEmpty(this.GetUsername(credentials)))
            {
                connectionStringBuilder.Remove("UID");
                connectionStringBuilder.Remove("UserName");
                connectionStringBuilder.Set("User ID", this.GetUsername(credentials));
                connectionStringBuilder.Remove("PWD");
                connectionStringBuilder.Set("Password", this.GetPassword(credentials));
            }

            if (!string.IsNullOrEmpty(customData))
            {
                connectionStringBuilder.Set("CustomData", customData);
            }

            if (!string.IsNullOrEmpty(localeIdentifier))
            {
                connectionStringBuilder.Set("Locale Identifier", localeIdentifier);
            }

            if (!string.IsNullOrEmpty(effectiveUserName))
            {
                connectionStringBuilder.Set("EffectiveUserName", effectiveUserName);
            }

            if (!string.IsNullOrEmpty(roles))
            {
                connectionStringBuilder.Set("Roles", roles);
            }

            this.connectionStrings[key] = connectionStringBuilder.ConnectionString;
            Log.Trace("Connection string: {0}", this.connectionStrings[key]);

            return new AdomdConnection(this.connectionStrings[key]);
        }

        private string GetUsername(string credentials)
        {
            if (string.IsNullOrEmpty(credentials))
            {
                return string.Empty;
            }

            string str = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            return str.Substring(0, str.IndexOf(':'));
        }

        private string GetPassword(string credentials)
        {
            if (string.IsNullOrEmpty(credentials))
            {
                return string.Empty;
            }

            string str = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            return str.Substring(str.IndexOf(':') + 1);
        }

        private MemberVO[] GetMembers(
          CellSet cellSet,
          int fromIndex,
          bool hasAllMember,
          string allMemberName,
          ConcurrentDictionary<string, MemberVO> allMembers)
        {
            List<MemberVO> memberVoList = new List<MemberVO>();
            if (cellSet.Axes.Count > 0)
            {
                if (!hasAllMember && allMembers.Count == 0)
                {
                    memberVoList.Add(new MemberVO()
                    {
                        uniqueName = allMemberName,
                        ordinal = 0,
                        levelOrdinal = 0,
                    });
                }

                foreach (Position position in (cellSet.Axes.Count == 2 ? cellSet.Axes[1] : cellSet.Axes[0]).Positions)
                {
                    if (position.Members.Count > 0)
                    {
                        MemberVO memberVo1 = MemberVO.FromItem(position.Members[0]);
                        if (allMembers.TryGetValue(memberVo1.uniqueName, out var memberVo2))
                        {
                            memberVo1.ordinal = memberVo2.ordinal;
                        }
                        else
                        {
                            memberVo1.ordinal = fromIndex;
                            ++fromIndex;
                        }

                        if (!hasAllMember)
                        {
                            if (allMembers.Count == 0)
                            {
                                if (memberVo1.levelOrdinal == 0)
                                {
                                    memberVo1.parentUniqueName = allMemberName;
                                }

                                ++memberVo1.ordinal;
                            }

                            ++memberVo1.levelOrdinal;
                        }

                        memberVoList.Add(memberVo1);
                    }
                }
            }

            return memberVoList.ToArray();
        }

        private int TimeDiff(DateTime start)
        {
            return (int)this.ToMilliseconds((DateTime.Now - start).Ticks);
        }

        private long ToMilliseconds(long ticks)
        {
            return (long)Math.Round(new TimeSpan(ticks).TotalMilliseconds);
        }

        private long GetSessionId(
          string catalog,
          string credentials,
          string cubeUniqueName,
          string customData,
          string localeIdentifier,
          string effectiveUserName,
          string roles)
        {
            long num = 0;
            using (AdomdConnection connection = this.CreateConnection(catalog, credentials, customData, localeIdentifier, effectiveUserName, roles))
            {
                connection.Open();
                CubeDef cube = connection.Cubes[cubeUniqueName];
                if (cube != null)
                {
                    DateTime dateTime = cube.LastProcessed;
                    long ticks1 = dateTime.Ticks;
                    dateTime = cube.LastUpdated;
                    long ticks2 = dateTime.Ticks;
                    num = this.ToMilliseconds(Math.Max(ticks1, ticks2));
                }

                connection.Close();
            }

            return num;
        }

        private bool IsSessionExpired(long sessionId, CubeDef cube)
        {
            if (!(cube != null))
            {
                return false;
            }

            long sessionId1 = sessionId;
            DateTime dateTime = cube.LastProcessed;
            long ticks1 = dateTime.Ticks;
            dateTime = cube.LastUpdated;
            long ticks2 = dateTime.Ticks;
            long milliseconds = this.ToMilliseconds(Math.Max(ticks1, ticks2));

            return this.IsSessionExpired(sessionId1, milliseconds);
        }

        private bool IsSessionExpired(long sessionId, long otherSession)
        {
            return sessionId < otherSession;
        }

        private HttpResponseMessage ComposeResponse(string json)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = (HttpContent)new StringContent(json, Encoding.UTF8, "application/json");

            return response;
        }
    }
}
