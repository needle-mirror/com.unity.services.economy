using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion
{
    class ConstructorNotFoundException : Exception
    {
        public ConstructorNotFoundException(Type type)
            : base($"Type {type.Name} must have a single public or internal constructor.")
        {
        }
    }
}
