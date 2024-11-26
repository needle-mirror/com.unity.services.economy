using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Services.Core.Editor.Environments;
using Unity.Services.Core.Editor.OrganizationHandler;
using Unity.Services.Economy.Editor.Authoring.Core.Logging;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using UnityEditor;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    interface IEconomyDashboardUrlResolver
    {
        Task<string> EconomyResource(string name);
    }

    class EconomyDashboardUrlResolver : IEconomyDashboardUrlResolver
    {
        readonly ILogger m_Logger;
        readonly IEnvironmentsApi m_EnvironmentsApi;
        readonly IOrganizationHandler m_OrganizationHandler;

        public EconomyDashboardUrlResolver(
            ILogger logger,
            IEnvironmentsApi environmentsApi,
            IOrganizationHandler organizationHandler)
        {
            m_Logger = logger;
            m_EnvironmentsApi = environmentsApi;
            m_OrganizationHandler  = organizationHandler;
        }

        string GetBaseUrl()
        {
            var projectId = CloudProjectSettings.projectId;;
            var envId = m_EnvironmentsApi.ActiveEnvironmentId;
            var orgId = m_OrganizationHandler.Key;
            return $"https://cloud.unity.com/home/organizations/{orgId}/projects/{projectId}/environments/{envId}/economy/configuration";
        }

        public Task<string> EconomyResource(string name)
        {
            return Task.FromResult($"{GetBaseUrl()}?config={name}");
        }
    }
}
