
using Api.ViewModels.Customer;
using Api.ViewModels.Parameter;

namespace Api.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public CustomerViewModel Customer { get; set; }

        public ParameterViewModel Type { get; set; }

        public double Amount { get; set; }
        public string Description { get; set; }

        public bool IsDebt { get; set; }

        public string DateText { get; set; }

        public string CreatedAtText { get; set; }

        public string ModifiedAtText { get; set; }
    }
}