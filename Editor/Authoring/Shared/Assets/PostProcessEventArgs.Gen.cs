using System;

namespace Unity.Services.Economy.Editor.Authoring.Shared.Assets
{
    record PostProcessEventArgs
    {
        public string[] ImportedAssetPaths = Array.Empty<string>();
        public string[] DeletedAssetPaths = Array.Empty<string>();
        public string[] MovedAssetPaths = Array.Empty<string>();
        public string[] MovedFromAssetPaths = Array.Empty<string>();
        public bool DidDomainReload;
    }
}
