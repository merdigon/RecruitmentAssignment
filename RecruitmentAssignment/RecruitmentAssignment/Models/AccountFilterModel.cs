using Newtonsoft.Json;
using System;

namespace RecruitmentAssignment.Models
{
    public class AccountFilterModel
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("Amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("PostingDate")]
        public DateTime? PostingDate { get; set; }

        [JsonProperty("IsCleared")]
        public bool? IsCleared { get; set; }

        [JsonProperty("ClearedDate")]
        public DateTime? ClearedDate { get; set; }
    }
}
