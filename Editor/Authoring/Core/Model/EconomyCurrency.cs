namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    class EconomyCurrency : EconomyResource
    {
        public override string EconomyType => EconomyResourceTypes.Currency;

        public long Initial { get; set; }

        public long? Max { get; set; }

        public override string EconomyExtension => EconomyResourcesExtensions.Currency;

        public override string Type => "Currency";

        public EconomyCurrency(string id)
            : base(id) { }
    }
}
