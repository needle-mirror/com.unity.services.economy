using UnityEditor;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    class ProjectIdProvider : IProjectIdProvider
    {
        public string ProjectId => CloudProjectSettings.projectId;
    }
}
