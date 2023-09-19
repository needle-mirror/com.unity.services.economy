using Unity.Services.Economy.Editor.Authoring.Core.Model;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Model
{
    [ScriptedImporter(1, new []
    {
        EconomyResourcesExtensions.Currency,
        EconomyResourcesExtensions.InventoryItem,
        EconomyResourcesExtensions.MoneyPurchase,
        EconomyResourcesExtensions.VirtualPurchase
    })]
    class EconomyAssetImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var asset = ScriptableObject.CreateInstance<EconomyAsset>();

            asset.Name = System.IO.Path.GetFileName(ctx.assetPath);

            ctx.AddObjectToAsset("MainAsset", asset);
            ctx.SetMainObject(asset);
        }
    }
}
