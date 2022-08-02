namespace Unity.Services.Economy.Model
{
    internal class ConfigurationMetadata
    {
        public string ConfigAssignmentHash { get; }

        internal ConfigurationMetadata(string configAssignmentHash)
        {
            ConfigAssignmentHash = configAssignmentHash;
        }
    }
}
