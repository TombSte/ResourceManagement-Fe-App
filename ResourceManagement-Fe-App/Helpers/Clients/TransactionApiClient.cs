
using ResourceManagement_Fe_App.Data;
using ResourceManagement_Fe_App.Data.Forms;
using ResourceManagement_Fe_App.Data.Transactions;

namespace ResourceManagement_Fe_App.Helpers.Clients
{
	public class TransactionApiClient
	{
		public readonly IRmHttpClient rmHttpClient;

		public TransactionApiClient(IRmHttpClient rmHttpClient)
		{
			this.rmHttpClient = rmHttpClient;
		}

		public async Task<TransactionResult> GetTransactionsAsync(int pageIndex = 0, int pageSize = 20, TransactionType? transactionType = null)
		{
			string transactionTypeQuery = transactionType.HasValue ? $"&TransactionType={transactionType.Value}" : "";

            var result = await this.rmHttpClient.GetAsync<TransactionResult>($"transaction/getall?PageSize={pageSize}&PageIndex={pageIndex}{transactionTypeQuery}");
			return result.Value;
		}

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var result = await this.rmHttpClient.GetAsync<IEnumerable<Category>>($"Category/getall");
            return result.Value;
        }
		public async Task<IEnumerable<SecondaryCategory>> GetSecondaryCategoriesAsync(int categoryId)
        {
            var result = await this.rmHttpClient.GetAsync<IEnumerable<SecondaryCategory>>($"Category/getallsecondary?categoryId={categoryId}");
            return result.Value;
        }

        public async Task AddTransactionAsync(TransactionFormData data)
		{
            await this.rmHttpClient.PostAsync($"transaction/add", data);
		}
	}
}
