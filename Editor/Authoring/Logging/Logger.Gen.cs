using Unity.Services.Economy.Editor.Authoring.Core.Logging;
using SharedLogger = Unity.Services.Economy.Editor.Authoring.Shared.Logging.Logger;
namespace Unity.Services.Economy.Editor.Authoring.Logging
{
    class Logger : ILogger
    {
        public void LogError(object message)
        {
            SharedLogger.LogError(message);
        }

        public void LogWarning(object message)
        {
            SharedLogger.LogWarning(message);
        }

        public void LogInfo(object message)
        {
            SharedLogger.Log(message);
        }

        public void LogVerbose(object message)
        {
            SharedLogger.LogVerbose(message);
        }
    }
}
