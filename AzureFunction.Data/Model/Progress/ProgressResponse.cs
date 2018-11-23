using System.Collections.Generic;
using System.Linq;

namespace AzureFunction.Data.Model.Progress
{
    public class ProgressResponse
    {
        public decimal QuestPointsEarned { get; set; }
        public decimal TotalQuestPercentCompleted { get; set; }
        public List<Milestone> MilestonesCompleted { get; set; }

        /// <summary>
        /// Maps the response.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MapResponse(ProgressEntity entity)
        {
            this.QuestPointsEarned = entity.QuestPointsEarned;
            this.TotalQuestPercentCompleted = entity.TotalQuestPercentCompleted;
            this.MilestonesCompleted = entity.MilestonesCompleted.ToList();
        }
    }
}