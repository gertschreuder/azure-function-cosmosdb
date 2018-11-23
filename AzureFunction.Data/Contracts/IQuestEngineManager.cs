using System.Threading.Tasks;
using AzureFunction.Data.Model.Notifications;
using AzureFunction.Data.Model.Progress;
using AzureFunction.Data.Model.State;

namespace AzureFunction.Data.Contracts
{
    public interface IQuestEngineManager
    {
        /// <summary>
        /// Updates the progress.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        ResultCollection UpdateProgress(ProgressRequest request);

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        ResultCollection GetState(StateRequest request);
    }
}
