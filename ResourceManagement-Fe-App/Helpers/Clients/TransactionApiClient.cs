
using AntDesign;
using OneOf.Types;
using ResourceManagement_Fe_App.Data;
using ResourceManagement_Fe_App.Data.ApiModel;
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

        public async Task<TransactionResult> GetTransactionsAsync(GetTransactions filters)
		{
			string query = filters.TransactionType.HasValue ? $"&TransactionType={filters.TransactionType.Value}" : "";

            var result = await this.rmHttpClient.PostAsync<GetTransactions, TransactionResult>($"transaction/getall",filters);
            if (!result.Success) await _notice.Error(new NotificationConfig
            {
                Message = "Errore! ",
                Description = "Impossibile recuperare le transazioni."
            });
            return result.Value;
        }

        public async Task<TransactionFormData> GetTransactionAsync(long id)
        {
            var result = await this.rmHttpClient.GetAsync<TransactionFormData>($"transaction?id={id}");
            if (!result.Success) await _notice.Error(new NotificationConfig
            {
                Message = "Errore! ",
                Description = "Impossibile recuperare la transazione"
            });
            return result.Value;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var result = await this.rmHttpClient.GetAsync<IEnumerable<Category>>($"Category/getall");
            return result.Value;
        }
		public async Task<IEnumerable<SecondaryCategory>> GetSecondaryCategoriesAsync(int categoryId)
        {
            var result = await this.rmHttpClient.GetAsync<IEnumerable<SecondaryCategory>>($"Category/getsecondarycategories?categoryId={categoryId}");
            return result.Value;
        }

        public async Task<IEnumerable<SecondaryCategory>> GetSecondaryCategoriesAsync()
        {
            var result = await this.rmHttpClient.GetAsync<IEnumerable<SecondaryCategory>>($"Category/getallsecondary");
            return result.Value;
        }

        public async Task AddTransactionAsync(TransactionFormData data)
		{
            var result = await this.rmHttpClient.PostAsync($"transaction/add", data);
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Transazione aggiunta con successo"
            });
        }
        public async Task UpdateTransactionAsync(TransactionFormData data)
		{
            var result = await this.rmHttpClient.PutAsync($"transaction", data);
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Transazione aggiornata con successo"
            });
        }

        public async Task UpdateCategoriesAsync(IEnumerable<Category> categories)
        {
            var result = await this.rmHttpClient.PutAsync($"category/updatemany", new UpdateCategories()
            {
                Categories = categories
            });
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categorie aggiornate con successo"
            });
        }
        public async Task UpdateSecondaryCategoriesAsync(IEnumerable<SecondaryCategory> categories)
        {
            var result = await this.rmHttpClient.PutAsync($"category/updatesecondarymany", new UpdateSecondaryCategories()
            {
                SecondaryCategories = categories
            });
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categorie aggiornate con successo"
            });
        }


        public async Task AddCategory(AddCategory data)
        {
            var result = await this.rmHttpClient.PostAsync($"category/add", data);
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria aggiunta con successo"
            });
        }

        public async Task AddSecondaryCategory(AddSecondaryCategory data)
        {
            var result = await this.rmHttpClient.PostAsync($"category/addsecondary", data);
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria secondaria aggiunta con successo"
            });
        }

        public async Task DeleteTransaction(long id)
        {
            var result = await this.rmHttpClient.DeleteAsync($"transaction/delete?id={id}");
            if (result.Success) _notice.Success(new NotificationConfig
            {
                Message = "Completato! ",
                Description = "Categoria secondaria aggiunta con successo"
            });
        }
    }
}
