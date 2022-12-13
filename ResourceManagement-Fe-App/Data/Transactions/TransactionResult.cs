namespace ResourceManagement_Fe_App.Data.Transactions
{
    public class TransactionResult
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public int TotalItems { get; set; }
    }
}
