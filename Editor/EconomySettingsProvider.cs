using Unity.Services.Core.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.Services.Economy
{
    class EconomySettingsProvider : EditorGameServiceSettingsProvider
    {
        const string k_Title = "Economy";
        const string k_GoToDashboardContainer = "dashboard-button-container";
        const string k_GoToDashboardBtn = "dashboard-link-button";

        protected override IEditorGameService EditorGameService => k_GameService;
        protected override string Title => k_Title;
        protected override string Description => "Economy is a cloud service that enables game developers to create their in-game economy and gives them seamless purchases, currency conversion, player inventory management, and more.";

        static readonly EconomyEditorGameService k_GameService = new EconomyEditorGameService();

        EconomySettingsProvider(SettingsScope scopes)
            : base(GenerateProjectSettingsPath(k_Title), scopes) {}

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new EconomySettingsProvider(SettingsScope.Project);
        }

        // This method must be implemented as part of EditorGameServiceSettingsProvider.
        // It is used to create UI elements in the window, but there's nothing to add atm so
        // it is essentially empty.
        protected override VisualElement GenerateServiceDetailUI()
        {
            VisualElement containerVisualElement = new VisualElement();

            return containerVisualElement;
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);
            SetDashboardButton(rootElement);
        }

        static void SetDashboardButton(VisualElement rootElement)
        {
            rootElement.Q(k_GoToDashboardContainer).style.display = DisplayStyle.Flex;
            var goToDashboard = rootElement.Q(k_GoToDashboardBtn);

            if (goToDashboard != null)
            {
                var clickable = new Clickable(() =>
                {
                    Application.OpenURL(k_GameService.GetFormattedDashboardUrl());
                });
                goToDashboard.AddManipulator(clickable);
            }
        }
    }
}
