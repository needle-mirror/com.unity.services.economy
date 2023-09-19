using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion
{
    interface IScopedServiceProvider : IServiceProvider, IDisposable
    {
        IScopedServiceProvider CreateScope();
    }
}
