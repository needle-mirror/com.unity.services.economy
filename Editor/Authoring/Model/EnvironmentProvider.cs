using Unity.Services.DeploymentApi.Editor;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    class EnvironmentProvider : IEnvironmentProvider
    {
        public string Current => Deployments.Instance.EnvironmentProvider.Current;
    }
}
