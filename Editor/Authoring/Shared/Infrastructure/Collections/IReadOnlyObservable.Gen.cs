using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Unity.Services.Economy.Editor.Authoring.Shared.Infrastructure.Collections
{
    interface IReadOnlyObservable<out T> : IReadOnlyList<T>, INotifyCollectionChanged, IDisposable
    {
    }
}
