using ResourceManagement_Fe_App.Data.Transactions;

namespace ResourceManagement_Fe_App.Data.ApiModel
{
    public record GetTransactions
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SearchText { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TransactionType? TransactionType { get; set; }
    }
}
