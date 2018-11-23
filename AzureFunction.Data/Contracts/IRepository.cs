using AzureFunction.Data.Model.Notifications;
using System;
using System.Threading.Tasks;

namespace AzureFunction.Data.Contracts
{
    public interface IRepository
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResultCollection> GetAsync<T>(Guid id) where T : class;

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResultCollection> InsertAsync<T>(T entity, string id) where T : class;

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResultCollection> UpdateAsync<T>(T entity, string id) where T : class;

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResultCollection> DeleteAsync<T>(Guid id) where T : class;
    }
}