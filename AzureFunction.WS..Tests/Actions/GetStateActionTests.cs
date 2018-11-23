using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Notifications;
using AzureFunction.Data.Model.State;
using AzureFunction.WS.Actions;
using AzureFunction.WS.Models;
using System;
using System.Threading.Tasks;
using AzureFunction.Data;
using AzureFunction.Data.Model.Progress;
using Assert = NUnit.Framework.Assert;

namespace AzureFunction.WS.Tests.Actions
{
    public class GetStateActionTests
    {
        private QuestEngineManager manager { get; set; }
        private Mock<IQuestEngineManager> mockedManager { get; set; }
        private Mock<IRepository> repository { get; set; }
        private Mock<ILogger> logger { get; set; }
        
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger>(MockBehavior.Loose);
            repository = new Mock<IRepository>(MockBehavior.Default);

            var result = ResultCollection.CreateEmpty();
            result.Payload = TestHelper.CreateProgressEntity();
            var id = new Guid(TestHelper.CreateProgressEntity().PlayerId);
            repository.Setup(r => r.GetAsync<ProgressEntity>(id)).Returns(Task.FromResult<ResultCollection>(result));

            manager = new QuestEngineManager(repository.Object);
            mockedManager = new Mock<IQuestEngineManager>();
        }

        [Test]
        public void OnSuccess()
        {
            var request = new StateRequest {PlayerId = new Guid(TestHelper.CreateProgressEntity().PlayerId) };
            var response = new StateResponse{ LastMilestoneIndexCompleted = -1, TotalQuestPercentCompleted = 0.00m};

            var action = new GetStateAction<dynamic>(manager, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnInvalid = (msg) => msg,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var result = actionResult as StateResponse;
            Assert.AreEqual(response.LastMilestoneIndexCompleted, result.LastMilestoneIndexCompleted);
            Assert.AreEqual(response.TotalQuestPercentCompleted, result.TotalQuestPercentCompleted);
        }

        [Test]
        public void OnFailed()
        {
            var request = new StateRequest { PlayerId = Guid.NewGuid() };
            var result = "OnFailed Test";
            mockedManager.Setup(s => s.GetState(request)).Throws(new Exception(result));

            var action = new GetStateAction<dynamic>(mockedManager.Object, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnInvalid = (msg) => msg,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            var error = actionResult as Exception;
            Assert.AreEqual(result, error?.Message);
        }

        [Test]
        public void OnInvalid()
        {
            var request = new StateRequest { PlayerId = Guid.Empty };
            var results = ResultCollection.CreateEmpty();
            mockedManager.Setup(s => s.GetState(request)).Returns(results);

            var action = new GetStateAction<dynamic>(mockedManager.Object, logger.Object)
            {
                OnFailed = (ex) => ex,
                OnInvalid = (msg) => msg,
                OnSuccess = (model) => model
            };

            var actionResult = action.Execute(request);
            Assert.AreEqual(Constants.ON_INVALID_ERROR, Convert.ToString(actionResult));
        }
    }
}
