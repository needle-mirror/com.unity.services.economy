using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Shared.Assets;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    /// <summary>
    /// This class serves to track creation and deletion of assets of the
    /// associated service type
    /// </summary>
    sealed class ObservableEconomyAssets : ObservableCollection<IDeploymentItem>, IDisposable
    {
        readonly ObservableAssets<EconomyAsset> m_MyServiceAssets;

        public ObservableEconomyAssets()
        {
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
                    Add(newItem);
                }
            }
        }
    }
}
