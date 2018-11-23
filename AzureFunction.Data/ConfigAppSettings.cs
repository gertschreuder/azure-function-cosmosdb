using System;
using System.Configuration;

namespace AzureFunction.Data
{
    public class ConfigAppSettings
    {
        public static string AzureWebJobsStorage => GetAppSettings("AzureWebJobsStorage");
        public static string AzureWebJobsDashboard => GetAppSettings("AzureWebJobsDashboard");
        public static string CosmosDBEndpoint => GetAppSettings("CosmosDBEndpoint");
        public static string CosmosDBAuthKey => GetAppSettings("CosmosDBAuthKey");
        public static string CosmosDBDatabase => GetAppSettings("CosmosDBDatabase");
        public static string CosmosDBProgressCollection => GetAppSettings("CosmosDBProgressCollection");

        /// <summary>
        /// Gets the quest rate from bet.
        /// </summary>
        /// <value>
        /// The quest rate from bet.
        /// </value>
        public static decimal QuestRateFromBet
        {
            get
            {
                var val = GetAppSettings("QuestRateFromBet");
                return decimal.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the quest level bonus rate.
        /// </summary>
        /// <value>
        /// The quest level bonus rate.
        /// </value>
        public static decimal QuestLevelBonusRate
        {
            get
            {
                var val = GetAppSettings("QuestLevelBonusRate");
                return decimal.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Converts to talquestpointsneeded.
        /// </summary>
        /// <value>
        /// The total quest points needed.
        /// </value>
        public static int TotalQuestPointsNeeded
        {
            get
            {
                var val = GetAppSettings("TotalQuestPointsNeeded");
                return int.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the amount quest milestones.
        /// </summary>
        /// <value>
        /// The amount quest milestones.
        /// </value>
        public static int AmountQuestMilestones
        {
            get
            {
                var val = GetAppSettings("AmountQuestMilestones");
                return int.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the milestone completion chip award.
        /// </summary>
        /// <value>
        /// The milestone completion chip award.
        /// </value>
        public static int MilestoneCompletionChipAward
        {
            get
            {
                var val = GetAppSettings("MilestoneCompletionChipAward");
                return int.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Configuration not set, value: {key}</exception>
        private static string GetAppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception)
            {
                throw new Exception($"Configuration not set, value: {key}");
            }
        }
    }
}
