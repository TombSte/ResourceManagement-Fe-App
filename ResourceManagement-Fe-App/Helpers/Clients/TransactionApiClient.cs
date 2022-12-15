
using AntDesign;
using OneOf.Types;
using ResourceManagement_Fe_App.Data;
using ResourceManagement_Fe_App.Data.Forms;
using ResourceManagement_Fe_App.Data.Transactions;

namespace ResourceManagement_Fe_App.Helpers.Clients
{
	public class TransactionApiClient
	{
		public readonly IRmHttpClient rmHttpClient;
        private readonly NotificationService _notice;

        public TransactionApiClient(IRmHttpClient rmHttpClient, NotificationService notice)
        {
            this.rmHttpClient = rmHttpClient;
            _notice = notice;
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
            var result = await this.rmHttpClient.PostAsync($"transaction/add", data);
            if (result.Success) await _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Transazione aggiunta con successo"
            });
        }

        public async Task AddCategory(AddCategory data)
        {
            var result = await this.rmHttpClient.PostAsync($"category/add", data);
            if (result.Success) await _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria aggiunta con successo"
            });
        }

        public async Task AddSecondaryCategory(AddSecondaryCategory data)
        {
            var result = await this.rmHttpClient.PostAsync($"category/addsecondary", data);
            if (result.Success) await _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria secondaria aggiunta con successo"
            });
        }

        public async Task DeleteTransaction(long id)
        {
            var result = await this.rmHttpClient.DeleteAsync($"transaction/delete?id={id}");
            if (result.Success) await _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria secondaria aggiunta con successo"
            });
        }
    }
}
