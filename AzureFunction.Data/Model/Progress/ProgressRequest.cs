using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureFunction.Data.Model.Progress
{
    public class ProgressRequest
    {
        public Guid PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public decimal ChipAmountBet { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return (PlayerId != Guid.Empty && PlayerLevel > 0 && ChipAmountBet >= 1m);
        }

        /// <summary>
        /// Calculates the quest progress.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void CalculateQuestProgress(ProgressEntity entity)
        {
            entity.QuestPointsEarned +=  (this.ChipAmountBet * ConfigAppSettings.QuestRateFromBet) +
                                        (this.PlayerLevel * ConfigAppSettings.QuestLevelBonusRate);

            var oldTotalQuestPercentCompleted = entity.TotalQuestPercentCompleted;
            var newTotalQuestPercentCompleted =
                entity.QuestPointsEarned / ConfigAppSettings.TotalQuestPointsNeeded * 100m;

            var iterations = (newTotalQuestPercentCompleted) - (newTotalQuestPercentCompleted % 100m);
            if (iterations > 0)
            {
                entity.PlayerLevel += Convert.ToInt16(iterations/100);
            }

            entity.TotalQuestPercentCompleted = newTotalQuestPercentCompleted % 100m;

            var milestonePercentage = 1m / ConfigAppSettings.AmountQuestMilestones * 100m;
            if (newTotalQuestPercentCompleted >= milestonePercentage)
            {
                var tempPercentage = (newTotalQuestPercentCompleted / milestonePercentage);
                decimal multiples = (tempPercentage - (tempPercentage % 1m));
                var milestonesAchieved = multiples - entity.MilestonesCompleted.ToList().Count;
                var lastMilestoneIndex = entity.MilestonesCompleted.Any() ? entity.MilestonesCompleted.OrderBy(m => m.MilestoneIndex).Last().MilestoneIndex : -1;
                for (int i = 0; i < Convert.ToInt16(milestonesAchieved); i++)
                {
                    lastMilestoneIndex++;
                    entity.MilestonesCompleted.Add(new Milestone { ChipsAwarded = ConfigAppSettings.MilestoneCompletionChipAward, MilestoneIndex = lastMilestoneIndex });
                }
            }
        }
    }
}
