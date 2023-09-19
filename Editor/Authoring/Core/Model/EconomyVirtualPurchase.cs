namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    class EconomyVirtualPurchase : EconomyResource
    {
        public override string EconomyType => EconomyResourceTypes.VirtualPurchase;

        public Cost[] Costs { get; set; }

        public Reward[] Rewards { get; set; }

        public override string EconomyExtension => EconomyResourcesExtensions.VirtualPurchase;

        public override string Type => "Virtual Purchase";

        public EconomyVirtualPurchase(string id)
            : base(id) { }
    }

    class Reward
    {
        public string ResourceId { get; set; }
        public long Amount { get; set; }
        public object DefaultInstanceData { get; set; }
    }

    class Cost
    {
        public string ResourceId { get; set; }
        public long Amount { get; set; }
    }
}
