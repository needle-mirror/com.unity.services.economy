using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Assets;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    /// <summary>
    /// This class serves to track creation and deletion of assets of the
    /// associated service type
    /// </summary>
    sealed class ObservableEconomyAssets : ObservableCollection<IDeploymentItem>, IDisposable
    {
        readonly IEconomyResourcesLoader m_ResourcesLoader;
        readonly ObservableAssets<EconomyAsset> m_MyServiceAssets;

        public ObservableEconomyAssets(IEconomyResourcesLoader resourcesLoader)
        {
            m_ResourcesLoader = resourcesLoader;
            m_MyServiceAssets = new ObservableAssets<EconomyAsset>();
            foreach (var asset in m_MyServiceAssets)
            {
                Add(asset);
            }
            m_MyServiceAssets.CollectionChanged += MyServiceAssetsOnCollectionChanged;
        }

        public void Dispose()
        {
            m_MyServiceAssets.CollectionChanged -= MyServiceAssetsOnCollectionChanged;
        }

        void MyServiceAssetsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems.Cast<EconomyAsset>())
                {
                    Remove(oldItem);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems.Cast<EconomyAsset>())
                {
                    OnNewAsset(newItem);
                }
            }
        }

        void OnNewAsset(EconomyAsset asset)
        {
            Add(asset);
            asset.BuildAndValidateEconomyResource(m_ResourcesLoader, CancellationToken.None).Wait();
        }

        // this method takes an asset being reloaded by the editor and recreate a scriptable object after having
        // transferred the data from the previous one
        EconomyAsset RegenAsset(EconomyAsset asset, string path)
        {
            // creating an asset is mandatory because the previous one is already partially destroyed at this point
            var newAsset = ScriptableObject.CreateInstance<EconomyAsset>();
            // we transfer the previous model instance to the new asset
            newAsset.Path = path;
            newAsset.Resource = asset.Resource;
            // we update the deserialization states
            newAsset.BuildAndValidateEconomyResource(m_ResourcesLoader, CancellationToken.None).Wait();
            // we don't modify this[] because the instance of IDeploymentItem hasn't changed.
            return newAsset;
        }

        public EconomyAsset GetOrCreateInstance(string ctxAssetPath)
        {
            foreach (var a in m_MyServiceAssets)
            {
                if (ctxAssetPath == a.Path)
                {
                    return a == null ? RegenAsset(a, ctxAssetPath) : a;
                }
            }
            var asset = ScriptableObject.CreateInstance<EconomyAsset>();
            asset.Path = ctxAssetPath;
            asset.BuildAndValidateEconomyResource(m_ResourcesLoader, CancellationToken.None).Wait();
            return asset;
        }
    }
}
