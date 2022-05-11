using System;
using Newtonsoft.Json.Linq;
using Unity.Services.RemoteConfig;

namespace Unity.Services.Economy
{
    interface IRemoteConfigRuntimeWrapper
    {
        IRuntimeConfigWrapper GetConfig(string key);
        void SetPlayerIdentityToken(string token);
        void FetchConfigs(string configType);
        string GetConfigAssignmentHash();
    }

    interface IRuntimeConfigWrapper
    {
        event Action<ConfigResponse> FetchCompleted;
        JObject config { get; }
        string GetJson(string key);
    }

    class RuntimeConfigSealedClassWrapper : IRuntimeConfigWrapper
    {
        RuntimeConfig m_RuntimeConfig;

        internal RuntimeConfigSealedClassWrapper(RuntimeConfig config)
        {
            m_RuntimeConfig = config;
        }

        public event Action<ConfigResponse> FetchCompleted
        {
            add { m_RuntimeConfig.FetchCompleted += value; }
            remove { m_RuntimeConfig.FetchCompleted -= value; }
        }

        public JObject config => m_RuntimeConfig.config;

        public string GetJson(string key)
        {
            return m_RuntimeConfig.GetJson(key);
        }
    }

    class RemoteConfigRuntimeNonStaticWrapper : IRemoteConfigRuntimeWrapper
    {
        struct UserAttributes {}
        struct AppAttributes {}

        public IRuntimeConfigWrapper GetConfig(string key)
        {
            return new RuntimeConfigSealedClassWrapper(RemoteConfigService.Instance.GetConfig(key));
        }

        public void SetPlayerIdentityToken(string token)
        {
            RemoteConfigService.Instance.SetPlayerIdentityToken(token);
        }

        public void FetchConfigs(string configType)
        {
            RemoteConfigService.Instance.FetchConfigs(configType, new UserAttributes(), new AppAttributes());
        }

        public string GetConfigAssignmentHash()
        {
            return RemoteConfigService.Instance.appConfig.configAssignmentHash;
        }
    }
}
