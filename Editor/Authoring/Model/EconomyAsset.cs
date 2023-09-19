using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.IO;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Deployment;
using Unity.Services.Economy.Editor.Authoring.Shared.Assets;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    class EconomyAsset : ScriptableObject, IPath, ISerializationCallbackReceiver, IDeploymentItem, ITypedItem
    {
        const string k_DefaultFileNameCurrency = "CURRENCY";
        const string k_DefaultFileNameInventoryItem = "INVENTORY_ITEM";
        const string k_DefaultFileNameVirtualPurchase = "VIRTUAL_PURCHASE";
        const string k_DefaultFileNameRealMoneyPurchase = "REAL_MONEY_PURCHASE";
        static readonly JsonSerializerSettings k_SerializerSettings = new ()
        {
            ContractResolver = new ContractResolver()
        };

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

        public void BuildAndValidateEconomyResource(IFileSystem fileSystem, IEconomyResourceSerializationUtility serializationUtility)
        {
            ResetDeploymentItem();

            if (Resource != null)
            {
                Resource.PropertyChanged -= OnEconomyResourcePropertyChanged;
            }

            Resource = serializationUtility.
                GetEconomyResourceFromJson(
                    System.IO.Path.GetFileNameWithoutExtension(Path),
                    fileSystem.ReadAllText(Path).Result,
                    System.IO.Path.GetExtension(Path),
                    out string message,
                    out string details);

            if (Resource != null)
            {
                Resource.PropertyChanged += OnEconomyResourcePropertyChanged;
            }
            else
            {
                Status = new DeploymentStatus(message, details, SeverityLevel.Error);
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

        [MenuItem("Assets/Create/Economy File/Currency", false, 81)]
        public static void CreateConfigCurrency()
        {
            var fileName = k_DefaultFileNameCurrency + EconomyResourcesExtensions.Currency;
            var currencyRequest = new CurrencyItemRequest(
                "CURRENCY",
                "CURRENCY",
                CurrencyItemRequest.TypeOptions.CURRENCY,
                1,
                50);
            ProjectWindowUtil.CreateAssetWithContent(
                fileName,
                JsonConvert.SerializeObject(currencyRequest, Formatting.Indented, k_SerializerSettings));
        }

        [MenuItem("Assets/Create/Economy File/Inventory Item", false, 81)]
        public static void CreateConfig()
        {
            var fileName = k_DefaultFileNameInventoryItem + EconomyResourcesExtensions.InventoryItem;
            var inventoryItemRequest = new InventoryItemRequest(
                "INVENTORY_ITEM",
                "INVENTORY_ITEM",
                InventoryItemRequest.TypeOptions.INVENTORYITEM);
            ProjectWindowUtil.CreateAssetWithContent(
                fileName,
                JsonConvert.SerializeObject(inventoryItemRequest, Formatting.Indented, k_SerializerSettings));
        }

        [MenuItem("Assets/Create/Economy File/Virtual Purchase", false, 81)]
        public static void CreateConfigVirtualPurchase()
        {
            var fileName = k_DefaultFileNameVirtualPurchase + EconomyResourcesExtensions.VirtualPurchase;
            var virtualPurchaseRequest = new VirtualPurchaseResourceRequest(
                "VIRTUAL_PURCHASE",
                "VIRTUAL_PURCHASE",
                VirtualPurchaseResourceRequest.TypeOptions.VIRTUALPURCHASE,
                new List<VirtualPurchaseResourceRequestCostsInner>
                {
                    new ("MY_RESOURCE_ID", 2)
                },
                new List<VirtualPurchaseResourceRequestRewardsInner>
                {
                    new ("MY_RESOURCE_ID_2", 6)
                });
            ProjectWindowUtil.CreateAssetWithContent(
                fileName,
                JsonConvert.SerializeObject(virtualPurchaseRequest, Formatting.Indented, k_SerializerSettings));
        }

        [MenuItem("Assets/Create/Economy File/Money Purchase", false, 81)]
        public static void CreateConfigMoneyPurchase()
        {
            var fileName = k_DefaultFileNameRealMoneyPurchase + EconomyResourcesExtensions.MoneyPurchase;
            var moneyPurchase = new RealMoneyPurchaseResourceRequest(
                "REAL_MONEY_PURCHASE",
                "REAL_MONEY_PURCHASE",
                RealMoneyPurchaseResourceRequest.TypeOptions.MONEYPURCHASE,
                new RealMoneyPurchaseItemRequestStoreIdentifiers(null, "123"),
                new List<RealMoneyPurchaseResourceRequestRewardsInner> {new("MY_RESOURCE_ID", 6)}
            );
            ProjectWindowUtil.CreateAssetWithContent(
                fileName,
                JsonConvert.SerializeObject(moneyPurchase, Formatting.Indented, k_SerializerSettings));
        }

        class ContractResolver : DefaultContractResolver
        {
            readonly string[] m_IgnoredPropertiesOnCreation = {"type", "id", "customData", "defaultInstanceData"};

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                var jsonPropertyAttribute = member.GetCustomAttribute<DataMemberAttribute>();
                if (jsonPropertyAttribute != null && m_IgnoredPropertiesOnCreation.Contains(jsonPropertyAttribute.Name))
                {
                    property.ShouldSerialize = _ => false;
                }

                return property;
            }
        }
    }
}
