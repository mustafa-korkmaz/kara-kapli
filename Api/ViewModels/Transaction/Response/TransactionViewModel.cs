
using Api.ViewModels.Customer.Response;
using Api.ViewModels.Parameter.Response;

namespace Api.ViewModels.Transaction.Response
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