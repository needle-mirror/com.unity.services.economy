using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion
{
    interface IServiceFactory
    {
        object Build(IServiceProvider provider, Scope scope);
    }
}
