using NUnit.Framework;
using AzureFunction.Data;

namespace AzureFunction.WS.Tests.Postman
{
    public class InitializeCosmosDB
    {
        [SetUp]
        public void Setup()
        {
            DocumentRepository.Initialize();
        }

        [Test]
        public void InsertTestRecordForPostman()
        {
            var repository = new DocumentRepository();
            var entity = TestHelper.CreatePostmanProgressEntity();
            var result = repository.InsertAsync(entity, entity.PlayerId).Result;
        }
    }
}
