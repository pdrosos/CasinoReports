namespace CasinoReports.Core.Models.Entities
{
    public class CasinoGame : BaseEquatableDeletableEntity<int, CasinoGame>
    {
        public CasinoGame(string name, int displayOrder)
            : this()
        {
            this.Name = name;
            this.DisplayOrder = displayOrder;
        }

        private CasinoGame()
        {
            // used by EF Core
        }

        public string Name { get; private set; }

        public int DisplayOrder { get; private set; }
    }
}
