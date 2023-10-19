namespace Unity.Services.Economy.Editor.Authoring.Analytics
{
    interface IEconomyEditorAnalytics
    {
        public void SendEvent(
            string action,
            string context = default,
            long duration = default,
            string exception = default);
    }
}
