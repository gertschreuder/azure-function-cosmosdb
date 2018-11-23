using AzureFunction.Data.Model.Progress;
using System.Collections.Generic;
using AzureFunction.Data;

namespace AzureFunction.WS.Tests
{
    public class TestHelper
    {
        public static string UnknownTestId = "02b03795-9cd8-4af6-ae38-978c4f4abbd9";

        /// <summary>
        /// Creates the progress entity.
        /// </summary>
        /// <returns></returns>
        public static ProgressEntity CreateProgressEntity()
        {
            return new ProgressEntity
            {
                PlayerId = "8850315c-1e29-4c84-81d6-f0fcb60f9a87",
                QuestPointsEarned = 0.00m,
                TotalQuestPercentCompleted = 0.00m,
                MilestonesCompleted = new List<Milestone>(),
                PlayerLevel = 1
            };
        }

        /// <summary>
        /// Creates the progress entity varient.
        /// </summary>
        /// <returns></returns>
        public static ProgressEntity CreateProgressEntityVarient()
        {
            var milestone = new Milestone
                {MilestoneIndex = 0, ChipsAwarded = ConfigAppSettings.MilestoneCompletionChipAward};
            return new ProgressEntity
            {
                PlayerId = "1150315c-1e29-4c84-81d2-f0fcb60f9a47",
                QuestPointsEarned = 253.00m,
                TotalQuestPercentCompleted = 25.30m,
                MilestonesCompleted = new List<Milestone>(){ milestone },
                PlayerLevel = 1
            };
        }

        /// <summary>
        /// Creates the postman progress entity.
        /// </summary>
        /// <returns></returns>
        public static ProgressEntity CreatePostmanProgressEntity()
        {
            return new ProgressEntity
            {
                PlayerId = "f9c619de-878c-48be-a481-72f942fb94dc",
                QuestPointsEarned = 10.00m,
                TotalQuestPercentCompleted = 1.00m,
                MilestonesCompleted = new List<Milestone>(),
                PlayerLevel = 1
            };
        }

        /// <summary>
        /// Updates the progress entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void UpdateProgressEntity(ProgressEntity entity)
        {
            entity.QuestPointsEarned += 1m;
            entity.TotalQuestPercentCompleted += 10m;
        }
    }
}
