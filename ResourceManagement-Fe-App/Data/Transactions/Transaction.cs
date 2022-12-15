namespace ResourceManagement_Fe_App.Data.Transactions
{
	public class TransactionShort
	{
        public long Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public string? CategoryName { get; set; }
        public string? SecondaryCategoryName { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
