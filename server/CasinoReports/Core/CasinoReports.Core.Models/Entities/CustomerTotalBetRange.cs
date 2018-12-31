namespace CasinoReports.Core.Models.Entities
{
    public class CustomerTotalBetRange : BaseEquatableDeletableEntity<int, CustomerTotalBetRange>
    {
        public CustomerTotalBetRange(string name, int displayOrder)
            : this()
        {
            this.Name = name;
            this.DisplayOrder = displayOrder;
        }

        private CustomerTotalBetRange()
        {
            // used by EF Core
        }

        public string Name { get; private set; }

        public int DisplayOrder { get; private set; }
    }
}
