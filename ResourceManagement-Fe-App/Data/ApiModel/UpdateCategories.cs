using ResourceManagement_Fe_App.Data.Transactions;

namespace ResourceManagement_Fe_App.Data.ApiModel
{
    public class UpdateCategories
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
