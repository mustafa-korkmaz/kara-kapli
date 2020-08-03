
using System;

namespace Api.ViewModels.Dashboard.Response
{
    public class UserDashboardViewModel
    {
        public int CustomerCount { get; set; }

        public int TransactionCount { get; set; }

        public double CustomerReceivablesTotal { get; set; }

        public double CustomerDebtsTotal { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
