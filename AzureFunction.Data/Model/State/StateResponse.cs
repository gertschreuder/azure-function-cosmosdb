using System.Collections.Generic;
using System.Linq;
using AzureFunction.Data.Model.Progress;

namespace AzureFunction.Data.Model.State
{
    public class StateResponse
    {
        public decimal TotalQuestPercentCompleted { get; set; }
        public int LastMilestoneIndexCompleted { get; set; }

        /// <summary>
        /// Maps the response.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MapResponse(ProgressEntity entity)
        {
            this.TotalQuestPercentCompleted = entity.TotalQuestPercentCompleted;
            this.LastMilestoneIndexCompleted = entity.MilestonesCompleted.Any() ? entity.MilestonesCompleted.OrderBy(m=>m.MilestoneIndex).Last().MilestoneIndex : -1;
        }
    }
}
