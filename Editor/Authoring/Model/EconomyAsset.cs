using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Analytics;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Model.File;
using Unity.Services.Economy.Editor.Authoring.Shared.Analytics;
using Unity.Services.Economy.Editor.Authoring.Shared.Assets;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    [HelpURL("https://docs.unity3d.com/Packages/com.unity.services.economy@3.4/manual/Authoring/index.html")]
    class EconomyAsset : ScriptableObject, IPath, ISerializationCallbackReceiver, IDeploymentItem, ITypedItem
    {
        const string k_DefaultFileNameCurrency = "CURRENCY";
        const string k_DefaultFileNameInventoryItem = "INVENTORY_ITEM";
        const string k_DefaultFileNameVirtualPurchase = "VIRTUAL_PURCHASE";
        const string k_DefaultFileNameRealMoneyPurchase = "REAL_MONEY_PURCHASE";

        [CanBeNull] public IEconomyResource Resource;

        // DeploymentItem properties
        string m_Name;
        string m_Type;
        string m_Path;
        float m_Progress;
        DeploymentStatus m_Status;

        // DeploymentItem Getters/Setters
        public string Name { get => m_Name; set => SetField(ref m_Name, value); }
        public string Type { get => m_Type; set => SetField(ref m_Type, value);  }
        public string Path { get => m_Path; set => SetField(ref m_Path, value, OnPathChanged); }
        public float Progress { get => m_Progress; set => SetField(ref m_Progress, value); }
        public DeploymentStatus Status { get => m_Status; set => SetField(ref m_Status, value); }

        public ObservableCollection<AssetState> States { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected EconomyAsset()
        {
            m_Progress = 0;
            m_Status = DeploymentStatus.Empty;
            States = new ObservableCollection<AssetState>();
            UpdateType(Path);
        }

        public EconomyAsset(string path)
            : this()
        {
            Path = path.Replace(
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.AltDirectorySeparatorChar);
        }

        // Using forwarding (with events) to keep this object's Status and Progress up to date with the IEconomyResource
        void OnEconomyResourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Resource == null)
            {
                return;
            }

            switch (e.PropertyName)
            {
                case nameof(Resource.Status):
                    Status = Resource.Status;
                    break;
                case nameof(Resource.Progress):
                    Progress = Resource.Progress;
                    break;
            }
        }

        void ResetDeploymentItem()
        {
            Status = new DeploymentStatus();
            Progress = 0f;
            States.Clear();
        }

        public async Task BuildAndValidateEconomyResource(
            IEconomyResourcesLoader resourceLoader,
            CancellationToken cancellationToken)
        {
            ResetDeploymentItem();

            if (Resource != null)
            {
                Resource.PropertyChanged -= OnEconomyResourcePropertyChanged;
            }

            try
            {
                Resource = await resourceLoader.LoadResourceAsync(Path, cancellationToken);
            }
            catch (AggregateException e)
            {
                var logger = EconomyAuthoringServices.Instance
                    .GetService<Unity.Services.Economy.Editor.Authoring.Core.Logging.ILogger>();
                logger.LogError($"Failed to load economy asset '{Path}'. Reason: {e.InnerException.Message}.");
            }

            if (Resource != null && Resource.Status.MessageSeverity != SeverityLevel.Error)
            {
                Resource !.PropertyChanged += OnEconomyResourcePropertyChanged;
            }

            if (Resource != null)
            {
                Status = Resource.Status;
                Progress = Resource.Progress;
            }
        }

        void OnPropertyChanged(string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        bool SetField<T>(
            ref T field,
            T value,
            Action onPropertyChanged = null,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            onPropertyChanged?.Invoke();
            return true;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { /* Not needed */ }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Name = System.IO.Path.GetFileName(Path);
            UpdateType(Path);
        }

        void OnPathChanged()
        {
            Name = System.IO.Path.GetFileName(Path);
            UpdateType(Path);
        }

        void UpdateType(string path)
        {
            switch (System.IO.Path.GetExtension(path))
            {
                case EconomyResourcesExtensions.Currency:
                    Type = "Currency";
                    break;
                case EconomyResourcesExtensions.InventoryItem:
                    Type = "Inventory Item";
                    break;
                case EconomyResourcesExtensions.VirtualPurchase:
                    Type = "Virtual Purchase";
                    break;
                case EconomyResourcesExtensions.MoneyPurchase:
                    Type = "Real Money Purchase";
                    break;
            }
        }

        [MenuItem("Assets/Create/Services/Economy Currency Configuration", false, 81)]
        public static void CreateConfigCurrency()
            => CreateFile(
                k_DefaultFileNameCurrency + EconomyResourcesExtensions.Currency,
                nameof(EconomyResourceTypes.Currency),
                new EconomyCurrencyFile().FileBodyText);

        [MenuItem("Assets/Create/Services/Economy Inventory Item Configuration", false, 81)]
        public static void CreateConfigInventoryItem()
            => CreateFile(
                k_DefaultFileNameInventoryItem + EconomyResourcesExtensions.InventoryItem,
                nameof(EconomyResourceTypes.InventoryItem),
                new EconomyInventoryItemFile().FileBodyText);

        [MenuItem("Assets/Create/Services/Economy Virtual Purchase Configuration", false, 81)]
        public static void CreateConfigVirtualPurchase()
            => CreateFile(
                k_DefaultFileNameVirtualPurchase + EconomyResourcesExtensions.VirtualPurchase,
                nameof(EconomyResourceTypes.VirtualPurchase),
                new EconomyVirtualPurchaseFile().FileBodyText);

        [MenuItem("Assets/Create/Services/Economy Money Purchase Configuration", false, 81)]
        public static void CreateConfigMoneyPurchase()
            => CreateFile(
                k_DefaultFileNameRealMoneyPurchase + EconomyResourcesExtensions.MoneyPurchase,
                nameof(EconomyResourceTypes.MoneyPurchase),
                new EconomyRealMoneyPurchaseFile().FileBodyText);

        static void CreateFile(string filename, string fileType, string content)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            ProjectWindowUtil.CreateAssetWithContent(filename, content);

            stopWatch.Stop();

            EconomyAuthoringServices.Instance.GetService<IEconomyEditorAnalytics>()
                .SendEvent("economy_file_created", fileType, stopWatch.ElapsedMilliseconds);
        }
    }
}
