using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion
{
    class MethodNotFoundException : Exception
    {
        public MethodNotFoundException(Type type, string methodName)
            : base($"Type {type.Name} must have a single public method called {methodName}")
        {
        }
    }
}
