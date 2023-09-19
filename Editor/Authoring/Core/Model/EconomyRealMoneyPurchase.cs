namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    class EconomyRealMoneyPurchase : EconomyResource
    {
        public override string EconomyType => EconomyResourceTypes.MoneyPurchase;

        public StoreIdentifiers StoreIdentifiers { get; set; }

        public RealMoneyReward[] Rewards { get; set; }

        public override string EconomyExtension => EconomyResourcesExtensions.MoneyPurchase;

        public override string Type => "Real Money Purchase";

        public EconomyRealMoneyPurchase(string id)
            : base(id) { }
    }

    class StoreIdentifiers
    {
        public string AppleAppStore { get; set; }
        public string GooglePlayStore { get; set; }
    }

    class RealMoneyReward
    {
        public string ResourceId { get; set; }
        public long Amount { get; set; }
    }
}
