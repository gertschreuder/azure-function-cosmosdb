using NUnit.Framework;
using AzureFunction.Data;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Progress;
using System;

namespace AzureFunction.WS.Tests.Integration
{
    public class RepositoryTests
    {
        private IRepository repository { get; set; }
        private ProgressEntity TestProgressEntity { get; set; }

        [SetUp]
        public void Setup()
        {
            repository = new DocumentRepository();
            TestProgressEntity = TestHelper.CreateProgressEntity();
            var result = repository.InsertAsync<ProgressEntity>(TestProgressEntity, TestProgressEntity.PlayerId).Result;
        }

        #region ProgressEntity Tests

        [Test]
        public void ValidProgressGet()
        {
            var results = repository.GetAsync<ProgressEntity>(new Guid(TestProgressEntity.PlayerId)).Result;
            var result = results.Payload as ProgressEntity;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.PlayerId, TestProgressEntity.PlayerId);
            Assert.AreEqual(result.PlayerLevel, TestProgressEntity.PlayerLevel);
            Assert.AreEqual(result.QuestPointsEarned, TestProgressEntity.QuestPointsEarned);
            Assert.AreEqual(result.TotalQuestPercentCompleted, TestProgressEntity.TotalQuestPercentCompleted);
        }

        [Test]
        public void InValidProgressGet()
        {
            try
            {
                var result = repository.GetAsync<ProgressEntity>(new Guid(TestHelper.UnknownTestId)).Result;
            }
            catch (AggregateException e)
            {
                Assert.IsTrue(e.InnerExceptions.Count > 0);
                Assert.AreEqual("One or more errors occurred.", e.Message);
            }
        }

        [Test]
        public void ValidProgressUpdate()
        {
            TestHelper.UpdateProgressEntity(TestProgressEntity);
            var results = repository.UpdateAsync<ProgressEntity>(TestProgressEntity, TestProgressEntity.PlayerId).Result;
            var result = results.Payload as ProgressEntity;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.PlayerId, TestProgressEntity.PlayerId);
            Assert.AreEqual(result.PlayerLevel, TestProgressEntity.PlayerLevel);
            Assert.AreEqual(result.QuestPointsEarned, TestProgressEntity.QuestPointsEarned);
            Assert.AreEqual(result.TotalQuestPercentCompleted, TestProgressEntity.TotalQuestPercentCompleted);
        }



        [Test]
        public void ValidVariantProgressUpdate()
        {
            TestProgressEntity = TestHelper.CreateProgressEntityVarient();
            TestHelper.UpdateProgressEntity(TestProgressEntity);
            var r = repository.InsertAsync<ProgressEntity>(TestProgressEntity, TestProgressEntity.PlayerId).Result;

            var results = repository.UpdateAsync<ProgressEntity>(TestProgressEntity, TestProgressEntity.PlayerId).Result;
            var result = results.Payload as ProgressEntity;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.PlayerId, TestProgressEntity.PlayerId);
            Assert.AreEqual(result.PlayerLevel, TestProgressEntity.PlayerLevel);
            Assert.AreEqual(result.QuestPointsEarned, TestProgressEntity.QuestPointsEarned);
            Assert.AreEqual(result.TotalQuestPercentCompleted, TestProgressEntity.TotalQuestPercentCompleted);
        }

        [Test]
        public void InValidProgressUpdate()
        {
            try
            {
                var result = repository.UpdateAsync<ProgressEntity>(new ProgressEntity(), TestHelper.UnknownTestId).Result;
            }
            catch (AggregateException e)
            {
                Assert.IsTrue(e.InnerExceptions.Count > 0);
                Assert.AreEqual("One or more errors occurred.", e.Message);
            }
        }

        #endregion ProgressEntity Tests

        [TearDown]
        public void Teardown()
        {
            var result = repository.DeleteAsync<ProgressEntity>(new Guid(TestProgressEntity.PlayerId)).Result;
            repository = null;
        }
    }
}