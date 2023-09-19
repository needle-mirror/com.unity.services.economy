using Unity.Services.DeploymentApi.Editor;

namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    interface IEconomyResource: IDeploymentItem, ITypedItem
    {
        string Id { get; }
        string EconomyType { get; }
        public object CustomData { get; set; }
        public new float Progress { get; set; }
    }
}
