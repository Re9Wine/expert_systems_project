namespace Domain.ViewEntity
{
    public class OperationCategoryView
    {
        public string Name { get; set; } = string.Empty;
        public decimal Outlay { get; set; } = decimal.Zero;
        public DateTime Date { get; set; } = DateTime.Now;


        public OperationCategoryView() { }

        public OperationCategoryView(string name, decimal outlay)
        {
            Name = name;
            Outlay = outlay;
        }

        public OperationCategoryView(DateTime date, decimal outlay)
        {
            Outlay = outlay;
            Date = date;
        }
    }
}
