using Unity.Services.Core.Editor.Environments;
using Unity.Services.Economy.Editor.Authoring.Shared.Analytics;

namespace Unity.Services.Economy.Editor.Authoring.Analytics
{
    class EconomyEditorAnalytics : IEconomyEditorAnalytics
    {
        const string k_Context = "DeploymentWindow";
        readonly IEnvironmentsApi m_EnvironmentsApi;
        readonly ICommonAnalytics m_CommonAnalytics;

        public EconomyEditorAnalytics(IEnvironmentsApi environmentsApi, ICommonAnalytics commonAnalytics)
        {
            m_EnvironmentsApi = environmentsApi;
            m_CommonAnalytics = commonAnalytics;
        }

        public void SendEvent(
            string action,
            string context = k_Context,
            long duration = default,
            string exception = default)
        {
            m_CommonAnalytics.Send(new ICommonAnalytics.CommonEventPayload
            {
                action = action,
                environment = m_EnvironmentsApi.ActiveEnvironmentId.ToString(),
                context = context,
                count = 1,
                exception = exception,
                duration = duration
            });
        }
    }
}
