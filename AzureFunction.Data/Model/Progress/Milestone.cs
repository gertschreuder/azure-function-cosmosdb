using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AzureFunction.Data.Model.Progress
{
    public class Milestone
    {
        [JsonProperty(PropertyName = "milestoneIndex")]
        public int MilestoneIndex { get; set; }
        [JsonProperty(PropertyName = "chipsAwarded")]
        public int ChipsAwarded { get; set; }
    }
}
