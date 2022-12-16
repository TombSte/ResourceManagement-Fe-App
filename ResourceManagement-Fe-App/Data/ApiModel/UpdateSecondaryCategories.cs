using ResourceManagement_Fe_App.Data.Transactions;

namespace ResourceManagement_Fe_App.Data.ApiModel
{
    public class UpdateSecondaryCategories
    {
        public IEnumerable<SecondaryCategory> SecondaryCategories { get; set; }
    }
}
