using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using AzureFunction.Data;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Notifications;
using AzureFunction.Data.Model.Progress;
using AzureFunction.WS.Actions;
using AzureFunction.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunction.WS.Tests.Actions
{
    public class UpdateProgressActionTests
    {
        private QuestEngineManager manager { get; set; }
        private Mock<IQuestEngineManager> mockedManager { get; set; }
        private Mock<IRepository> repository { get; set; }
        private Mock<ILogger> logger { get; set; }
        private ProgressEntity TestProgressEntity { get; set; }

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger>(MockBehavior.Loose);
            repository = new Mock<IRepository>(MockBehavior.Default);
            CreateQuestEngineManager(TestHelper.CreateProgressEntity());
            mockedManager = new Mock<IQuestEngineManager>();
        }

        [Test]
        public void OnSuccess()
        {
            var request = new ProgressRequest
            {
                PlayerId = new Guid(TestProgressEntity.PlayerId),
                PlayerLevel = TestProgressEntity.PlayerLevel,
                ChipAmountBet = 50.00m
            };
            var response = new ProgressResponse
            {
                TotalQuestPercentCompleted = 6.11m,
                QuestPointsEarned = 61.10m,
                MilestonesCompleted = new List<Milestone>() 
            };
           
            var action = new UpdateProgressAction<dynamic>(manager, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var result = actionResult as ProgressResponse;

            Assert.AreEqual(response.QuestPointsEarned, result.QuestPointsEarned);
            Assert.AreEqual(response.TotalQuestPercentCompleted, result.TotalQuestPercentCompleted);
            Assert.False(result.MilestonesCompleted.Any());
            Assert.AreEqual(response.MilestonesCompleted.Count, result.MilestonesCompleted.Count);
        }

        [Test]
        public void OnSuccessVarient()
        {
            TestProgressEntity = TestHelper.CreateProgressEntityVarient();
            CreateQuestEngineManager(TestProgressEntity);
            var request = new ProgressRequest
            {
                PlayerId = new Guid(TestProgressEntity.PlayerId),
                PlayerLevel = TestProgressEntity.PlayerLevel,
                ChipAmountBet = 500.00m
            };
            var milestones = new List<Milestone>{ new Milestone { ChipsAwarded = 2500, MilestoneIndex = 1 }, new Milestone { ChipsAwarded = 2500, MilestoneIndex = 2 }};
            var response = new ProgressResponse
            {
                TotalQuestPercentCompleted = 85.41m,
                QuestPointsEarned = 854.10m,
                MilestonesCompleted = TestProgressEntity.MilestonesCompleted
            };
            response.MilestonesCompleted.AddRange(milestones);

            var action = new UpdateProgressAction<dynamic>(manager, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var result = actionResult as ProgressResponse;

            Assert.AreEqual(response.QuestPointsEarned, result.QuestPointsEarned);
            Assert.AreEqual(response.TotalQuestPercentCompleted, result.TotalQuestPercentCompleted);
            Assert.True(result.MilestonesCompleted.Any());
            Assert.AreEqual(response.MilestonesCompleted.Count, result.MilestonesCompleted.Count);
            Assert.AreEqual(response.MilestonesCompleted[0].ChipsAwarded, result.MilestonesCompleted[0].ChipsAwarded);
            Assert.AreEqual(response.MilestonesCompleted[1].ChipsAwarded, result.MilestonesCompleted[1].ChipsAwarded);
            Assert.AreEqual(response.MilestonesCompleted[2].ChipsAwarded, result.MilestonesCompleted[2].ChipsAwarded);
            Assert.AreEqual(response.MilestonesCompleted[0].MilestoneIndex, result.MilestonesCompleted[0].MilestoneIndex);
            Assert.AreEqual(response.MilestonesCompleted[1].MilestoneIndex, result.MilestonesCompleted[1].MilestoneIndex);
            Assert.AreEqual(response.MilestonesCompleted[2].MilestoneIndex, result.MilestonesCompleted[2].MilestoneIndex);
        }

        [Test]
        public void OnValidationFailed()
        {
            var request = new ProgressRequest();
            var results = ResultCollection.CreateEmpty();
            mockedManager.Setup(s => s.UpdateProgress(request)).Returns(results);

            var action = new UpdateProgressAction<dynamic>(mockedManager.Object, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var error = actionResult as Exception;
            Assert.AreEqual(Constants.PROGRESS_VALIDATION_ERROR, error?.Message);
        }

        [Test]
        public void OnFailed()
        {
            var request = new ProgressRequest { PlayerId = Guid.NewGuid(), PlayerLevel = 1, ChipAmountBet = 1.00m};
            var result = "OnFailed Test";
            mockedManager.Setup(s => s.UpdateProgress(request)).Throws(new Exception(result));

            var action = new UpdateProgressAction<dynamic>(mockedManager.Object, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var error = actionResult as Exception;
            Assert.AreEqual(result, error?.Message);
        }

        /// <summary>
        /// Creates the quest engine manager.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void CreateQuestEngineManager(ProgressEntity entity)
        {
            TestProgressEntity = entity;
            var result = ResultCollection.CreateEmpty();
            result.Payload = TestProgressEntity;
            var id = new Guid(TestProgressEntity.PlayerId);
            repository.Setup(r => r.GetAsync<ProgressEntity>(id)).Returns(Task.FromResult<ResultCollection>(result));
            repository.Setup(r => r.UpdateAsync<ProgressEntity>(TestProgressEntity, TestProgressEntity.PlayerId)).Returns(Task.FromResult<ResultCollection>(result));

            manager = new QuestEngineManager(repository.Object);
        }
    }
}
