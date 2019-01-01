USE [CasinoReports]
GO

CREATE TABLE [dbo].[DimDate] 
(
    DateKey                 [int] NOT NULL,
    FullDate                [date] NOT NULL,
    DayNumberOfWeek         [tinyint] NOT NULL,
    DayNameOfWeek           [nvarchar](10) NOT NULL,
    WeekDayType             [nvarchar](7) NOT NULL,
    DayNumberOfMonth        [tinyint] NOT NULL,
    DayNumberOfYear         [smallint] NOT NULL,
    WeekNumberOfYear        [tinyint] NOT NULL,
    MonthNameOfYear         [nvarchar](10) NOT NULL,
    MonthNumberOfYear       [tinyint] NOT NULL,
    CalendarQuarterNumber   [tinyint] NOT NULL,
    CalendarQuarterName     [nchar](2) NOT NULL,
    CalendarSemesterNumber  [tinyint] NOT NULL,
    CalendarSemesterName    [nvarchar](15) NOT NULL,
    CalendarYear            [smallint] NOT NULL,
    FiscalMonthNumber       [tinyint] NOT NULL,
    FiscalQuarterNumber     [tinyint] NOT NULL,
    FiscalQuarterName       [nchar](2) NOT NULL,
    FiscalSemesterNumber    [tinyint] NOT NULL,
    FiscalSemesterName      [nvarchar](15) NOT NULL,
    FiscalYear              [smallint] NOT NULL
    CONSTRAINT [PK_DimDate_DateKey] PRIMARY KEY CLUSTERED  
    (
        [DateKey] ASC
    )
) 
GO

BEGIN TRAN

DECLARE @DateCalendarStart  datetime,
        @DateCalendarEnd    datetime,
        @FiscalCounter      datetime,
        @FiscalMonthOffset  int;
 
SET @DateCalendarStart = '2015-01-01';
SET @DateCalendarEnd = '2040-12-31';
 
-- Set this to the number of months to add or extract to the current date to get the beginning 
-- of the Fiscal Year. Example: If the Fiscal Year begins July 1, assign the value of 6 
-- to the @FiscalMonthOffset variable. Negative values are also allowed, thus if your 
-- 2012 Fiscal Year begins in July of 2011, assign a value of -6.
SET @FiscalMonthOffset = 6;
 
WITH DateDimension
AS
(
    SELECT  @DateCalendarStart AS DateCalendarValue,
            dateadd(m, @FiscalMonthOffset, @DateCalendarStart) AS FiscalCounter
                 
    UNION ALL
     
    SELECT  DateCalendarValue + 1,
            dateadd(m, @FiscalMonthOffset, (DateCalendarValue + 1)) AS FiscalCounter
    FROM    DateDimension 
    WHERE   DateCalendarValue + 1 < = @DateCalendarEnd
)
INSERT INTO [dbo].[DimDate] ([DateKey], [FullDate], [DayNumberOfWeek], [DayNameOfWeek], [WeekDayType],
                        [DayNumberOfMonth], [DayNumberOfYear], [WeekNumberOfYear], [MonthNameOfYear],
                        [MonthNumberOfYear], [CalendarQuarterNumber], [CalendarQuarterName], [CalendarSemesterNumber],
                        [CalendarSemesterName], [CalendarYear], [FiscalMonthNumber], [FiscalQuarterNumber],
                        [FiscalQuarterName], [FiscalSemesterNumber], [FiscalSemesterName], [FiscalYear])
SELECT  cast(convert(varchar(25), DateCalendarValue, 112) AS int) AS 'DateKey',
        cast(DateCalendarValue as date) AS 'FullDate',
        datepart(weekday, DateCalendarValue) AS 'DayNumberOfWeek',
        datename(weekday, DateCalendarValue) AS 'DayNameOfWeek',
        CASE datename(dw, DateCalendarValue)
            WHEN 'Saturday' THEN 'Weekend'
            WHEN 'Sunday' THEN 'Weekend'
        ELSE 'Weekday'
        END AS 'WeekDayType',
        datepart(day, DateCalendarValue) AS 'DayNumberOfMonth',
        datepart(dayofyear, DateCalendarValue) AS 'DayNumberOfYear',
        datepart(week, DateCalendarValue) AS 'WeekNumberOfYear',
        datename(month, DateCalendarValue) AS 'MonthNameOfYear',
        datepart(month, DateCalendarValue) AS 'MonthNumberOfYear',
        datepart(quarter, DateCalendarValue) AS 'CalendarQuarterNumber',
        'Q' + cast(datepart(quarter, DateCalendarValue) AS nvarchar) AS 'CalendarQuarterName',
        CASE
            WHEN datepart(month, DateCalendarValue) <= 6 THEN 1
            WHEN datepart(month, DateCalendarValue) > 6 THEN 2
        END AS 'CalendarSemesterNumber',
        CASE
            WHEN datepart(month, DateCalendarValue) < = 6 THEN 'First Semester'
            WHEN datepart(month, DateCalendarValue) > 6 THEN 'Second Semester' 
        END AS 'CalendarSemesterName',
        datepart(year, DateCalendarValue) AS 'CalendarYear',
        datepart(month, FiscalCounter) AS 'FiscalMonthNumber',
        datepart(quarter, FiscalCounter) AS 'FiscalQuarterNumber',
        'Q' + cast(datepart(quarter, FiscalCounter) AS nvarchar) AS 'FiscalQuarterName',  
        CASE
            WHEN datepart(month, FiscalCounter) < = 6 THEN 1
            WHEN datepart(month, FiscalCounter) > 6 THEN 2 
        END AS 'FiscalSemesterNumber',  
        CASE
            WHEN datepart(month, FiscalCounter) < = 6 THEN 'First Semester'
            WHEN  datepart(month, FiscalCounter) > 6 THEN 'Second Semester'
        END AS 'FiscalSemesterName',            
        datepart(year, FiscalCounter) as 'FiscalYear'
FROM    DateDimension
ORDER BY
        DateCalendarValue
OPTION (maxrecursion 0);

COMMIT TRAN
