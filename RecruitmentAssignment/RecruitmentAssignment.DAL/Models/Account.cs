using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAssignment.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public AccountType Type { get; set; }

        public AccountSummary Summary { get; set; }

        public decimal Amount { get; set; }

        public DateTime PostingDate { get; set; }

        public bool IsCleared { get; set; }

        public DateTime? ClearedDate { get; set; }
    }
}
