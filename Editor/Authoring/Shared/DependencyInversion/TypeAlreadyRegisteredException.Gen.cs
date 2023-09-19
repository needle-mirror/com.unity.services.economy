using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion
{
    class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException(Type type)
            : base($"A factory for type {type.Name} has already been registered")
        {
        }
    }
}
