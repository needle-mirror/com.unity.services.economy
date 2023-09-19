namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    class EconomyInventoryItem : EconomyResource
    {
        public override string EconomyType => EconomyResourceTypes.InventoryItem;

        public override string EconomyExtension => EconomyResourcesExtensions.InventoryItem;

        public override string Type => "Inventory Item";

        public EconomyInventoryItem(string id)
            : base(id) { }
    }
}
