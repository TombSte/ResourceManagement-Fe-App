namespace ResourceManagement_Fe_App.Data.Forms
{
    public class TransactionFormData
    {
        public double Amount { get; set; }
        public string Title { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int CategoryId { get; set; }
        public int SecondCategoryId { get; set; }
        public TransactionType Type { get; set; }
    }
}
