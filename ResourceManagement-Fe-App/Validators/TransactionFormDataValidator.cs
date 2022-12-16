
using FluentValidation;
using ResourceManagement_Fe_App.Data.Forms;

namespace ResourceManagement_Fe_App.Validators
{   
    public class TransactionFormDataValidator : AbstractValidator<TransactionFormData>
    {
        public TransactionFormDataValidator()
        {
            RuleFor(t => t.Title).NotNull().NotEmpty();
            RuleFor(t => t.Amount).GreaterThanOrEqualTo(0);
            RuleFor(t => t.CategoryId).NotNull().GreaterThan(0);
            RuleFor(t => t.TransactionDate).Must((x,c) => {
                if (!c.HasValue) return false;
                if (c.Value.Year < 2000) return false;
                return true;
            });
        }
    }
}
