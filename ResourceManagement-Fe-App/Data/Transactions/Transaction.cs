namespace ResourceManagement_Fe_App.Data.Transactions
{
	public class Transaction
	{
		public string Title { get; set; }
		public double Amount { get; set; }
		public DateTime TransactionDate { get; set; }
		public TransactionType TransactionType { get; set; }

		public IEnumerable<Category> Categories { get; set; }
	}
}
