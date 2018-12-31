namespace CasinoReports.Core.Models.Entities
{
    public class CasinoPlayerType : BaseEquatableDeletableEntity<int, CasinoPlayerType>
    {
        public CasinoPlayerType(string name, int displayOrder)
            : this()
        {
            this.Name = name;
            this.DisplayOrder = displayOrder;
        }

        private CasinoPlayerType()
        {
            // used by EF Core
        }

        public string Name { get; private set; }

        public int DisplayOrder { get; private set; }
    }
}
