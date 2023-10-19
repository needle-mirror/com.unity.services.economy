using System;
using System.Text;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Http;
using Unity.Services.Economy.Editor.Authoring.Core.Deploy;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using ILogger = Unity.Services.Economy.Editor.Authoring.Core.Logging.ILogger;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class EditorEconomyDeploymentHandler : EconomyDeploymentHandler
    {
        internal const string k_ActionableDeserializationErrorMsg =
            "Make sure the file's content is valid JSON and follows the structure of Economy resources or try again later.";

        internal const string k_ActionBadRequestErrorMsg =
            "If your Real Money Purchase and Virtual Purchase resource depends on other resources, make sure that" +
            " they exist before deploying this resource.";

        internal EditorEconomyDeploymentHandler(IEconomyClient client, ILogger logger) : base(client, logger)
        {
        }

        internal override void HandleException(Exception exception, IEconomyResource resource, DeployResult result)
        {
            result.Failed.Add(resource);

            switch (exception)
            {
                case HttpException httpException:
                    var errorMsg = "";

                    try
                    {
                        errorMsg = Encoding.UTF8.GetString(httpException.Response.Data);

                        if (resource.EconomyType is EconomyResourceTypes.VirtualPurchase or
                            EconomyResourceTypes.MoneyPurchase)
                        {
                            errorMsg += " - " + k_ActionBadRequestErrorMsg;
                        }
                    }
                    catch { /* Silently fail */ }

                    resource.Status =
                        new DeploymentStatus(
                            httpException.Message,
                            errorMsg,
                            SeverityLevel.Error);
                    break;
                default:
                    resource.Status =
                        new DeploymentStatus(
                            exception.Message,
                            string.Join(
                                " - ",
                                exception.InnerException?.Message, k_ActionableDeserializationErrorMsg),
                            SeverityLevel.Error);
                    break;
            }
        }
    }
}
