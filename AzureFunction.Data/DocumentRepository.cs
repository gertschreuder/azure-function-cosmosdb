using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using AzureFunction.Data.Contracts;
using AzureFunction.Data.Model.Notifications;
using AzureFunction.Data.Model.Progress;
using System;
using System.Threading.Tasks;

namespace AzureFunction.Data
{
    public class DocumentRepository : IRepository
    {
        private readonly string DatabaseId = ConfigAppSettings.CosmosDBDatabase;
        private readonly string ProgressCollectionId = ConfigAppSettings.CosmosDBProgressCollection;
        private readonly DocumentClient client;
        private readonly ResultCollection result;
        
        public DocumentRepository()
        {
            client = new DocumentClient(new Uri(ConfigAppSettings.CosmosDBEndpoint), ConfigAppSettings.CosmosDBAuthKey, new ConnectionPolicy { EnableEndpointDiscovery = false });
            result = ResultCollection.CreateEmpty();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ResultCollection> GetAsync<T>(Guid id) where T : class
        {
            Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ResolveCollection<T>(), id.ToString()));
            result.Payload = (T)(dynamic)document;
            return result;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ResultCollection> InsertAsync<T>(T entity, string id) where T : class
        {
            Document document = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, ResolveCollection<T>()), entity);
            result.Payload = (T)(dynamic)document;
            return result;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ResultCollection> UpdateAsync<T>(T entity, string id) where T : class
        {
            Document document = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ResolveCollection<T>(), id), entity);
            result.Payload = (T)(dynamic)document;
            return result;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ResultCollection> DeleteAsync<T>(Guid id) where T : class
        {
            Document document = await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ResolveCollection<T>(), id.ToString()));
            result.Payload = (T)(dynamic)document;
            return result;
        }

        #region Initialize DB

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            var client = new DocumentClient(new Uri(ConfigAppSettings.CosmosDBEndpoint), ConfigAppSettings.CosmosDBAuthKey, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync(client).Wait();
            CreateCollectionIfNotExistsAsync(client).Wait();
        }

        /// <summary>
        /// Creates the database if not exists asynchronous.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        private static async Task CreateDatabaseIfNotExistsAsync(DocumentClient client)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(ConfigAppSettings.CosmosDBDatabase));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = ConfigAppSettings.CosmosDBDatabase });
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Creates the collection if not exists asynchronous.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        private static async Task CreateCollectionIfNotExistsAsync(DocumentClient client)
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(ConfigAppSettings.CosmosDBDatabase, ConfigAppSettings.CosmosDBProgressCollection));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(ConfigAppSettings.CosmosDBDatabase),
                        new DocumentCollection { Id = ConfigAppSettings.CosmosDBProgressCollection },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion Initialize DB

        /// <summary>
        /// Resolves the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception">Unknown entity type.</exception>
        private string ResolveCollection<T>() where T : class
        {
            var entityType = typeof(T);

            if (entityType == typeof(ProgressEntity))
            {
                return ProgressCollectionId;
            }

            throw new Exception("Unknown entity type.");
        }
    }
}