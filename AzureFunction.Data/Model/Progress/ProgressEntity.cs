using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AzureFunction.Data.Model.Progress
{
    public class ProgressEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string PlayerId { get; set; }

        [JsonProperty(PropertyName = "playerLevel")]
        public int PlayerLevel { get; set; }

        [JsonProperty(PropertyName = "questPointsEarned")]
        public decimal QuestPointsEarned { get; set; }

        [JsonProperty(PropertyName = "totalQuestPercentCompleted")]
        public decimal TotalQuestPercentCompleted { get; set; }

        [JsonProperty(PropertyName = "milestonesCompleted")]
        public List<Milestone> MilestonesCompleted { get; set; }
    }
}
