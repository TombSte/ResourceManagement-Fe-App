
using ResourceManagement_Fe_App.Data;
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

            return await this.rmHttpClient.GetAsync<TransactionResult>($"transaction/getall?PageSize={pageSize}&PageIndex={pageIndex}{transactionTypeQuery}");
		}
	}
}
