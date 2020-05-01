
using Api.ViewModels.Customer;
using Api.ViewModels.Parameter;

namespace Api.ViewModels.CustomerOperation
{
    public class CustomerOperationViewModel
    {
        public int Id { get; set; }

        public CustomerViewModel Customer { get; set; }

        public ParameterViewModel Type { get; set; }

        public double Amount { get; set; }
        public string Description { get; set; }

        public bool IsReceivable { get; set; }

        public bool IsCompleted { get; set; }

        public string DateText { get; set; }

        public string CreatedAtText { get; set; }

        public string ModifiedAtText { get; set; }
    }
}