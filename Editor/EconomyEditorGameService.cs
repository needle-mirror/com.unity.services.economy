using Unity.Services.Core.Editor;
using Unity.Services.Core.Editor.OrganizationHandler;
using UnityEditor;

namespace Unity.Services.Economy
{
    struct EconomyIdentifier : IEditorGameServiceIdentifier
    {
        public string GetKey() => "Economy";
    }

    class EconomyEditorGameService : IEditorGameService
    {
        public string Name => "Economy";
        public IEditorGameServiceIdentifier Identifier => k_Identifier;
        public bool RequiresCoppaCompliance => false;
        public bool HasDashboard => true;
        public IEditorGameServiceEnabler Enabler => null;

        static readonly EconomyIdentifier k_Identifier = new EconomyIdentifier();

        public string GetFormattedDashboardUrl()
        {
            return $"https://dashboard.unity3d.com/organizations/{OrganizationProvider.Organization.Key}/projects/{CloudProjectSettings.projectId}/economy/about";
        }
    }
}
