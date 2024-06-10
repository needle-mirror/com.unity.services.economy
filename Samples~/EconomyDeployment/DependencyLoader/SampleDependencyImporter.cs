using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageManagerClient = UnityEditor.PackageManager.Client;

class SampleDependencyImporter
{
    const string k_SampleDependencyFilename = "sample-dependencies.json";
    const string k_TextMeshProPackageName = "com.unity.textmeshpro";
    const string k_UguiPackageId = "com.unity.ugui@2.0.0";

    [InitializeOnLoadMethod]
    static void Initialization()
    {
        RunSampleDependencyImporter();
    }

    static void RunSampleDependencyImporter(
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
    {
        var scriptsDirectory = Directory.GetParent(sourceFilePath);
        var dependenciesFilepath = Path.Combine(scriptsDirectory!.Parent!.FullName, k_SampleDependencyFilename);
        InstallPackageDependencies(dependenciesFilepath);
        DeleteFolder(scriptsDirectory!.FullName);
    }

    /// <summary>
    /// Install packages from Sample Configuration file
    /// </summary>
    /// <param name="sampleConfigFilepath">filepath to Sample Configuration file</param>
    static void InstallPackageDependencies(string sampleConfigFilepath)
    {
        if (File.Exists(sampleConfigFilepath))
        {
            var configurationText = File.ReadAllText(sampleConfigFilepath);
            var packagesToInstall = GetSampleDependenciesToInstall(configurationText);
            InstallMultiplePackages(packagesToInstall);
            AssetDatabase.Refresh();
        }
    }

    static List<string> GetSampleDependenciesToInstall(string configurationText)
    {
        var sampleConfig = JsonUtility.FromJson<JsonListWrapper<string>>(configurationText).dependencies;

        var packageList = PackageManagerClient.List(true);
        while (!packageList.IsCompleted);

        var packagesToInstall = new List<string>();
        foreach (var packageId in sampleConfig)
        {
            if (HasHigherVersion(packageId, packageList))
            {
                packagesToInstall.Add(packageId);
            }
        }

        return packagesToInstall;
    }

    static void InstallMultiplePackages(List<string> packageIds)
    {
#if UNITY_2023_3_OR_NEWER
        FilterTextMeshProIfNecessary(packageIds);
#endif

        if (packageIds.Any())
        {
            var packageAdd = PackageManagerClient.AddAndRemove(packageIds.ToArray(), null);
            while (!packageAdd.IsCompleted) {}

            foreach (var packageId in packageIds)
            {
                Debug.Log($"Installed package {packageId}.");
            }
        }

#if UNITY_2023_3_OR_NEWER
        if (packageIds.Contains(k_UguiPackageId))
        {
            var path = Path.GetFullPath("Packages/com.unity.ugui/Package Resources/TMP Essential Resources.unitypackage");
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.ImportPackage(path, false);
            }
        }
#endif
    }

#if UNITY_2023_3_OR_NEWER
    static void FilterTextMeshProIfNecessary(List<string> packageIds)
    {
        string packageToRemove = null;
        foreach (var packageId in packageIds)
        {
            if (packageId.StartsWith(k_TextMeshProPackageName))
            {
                packageToRemove = packageId;
                packageIds.Remove(k_TextMeshProPackageName);
            }
        }

        if (!string.IsNullOrEmpty(packageToRemove))
        {
            packageIds.Remove(packageToRemove);
            packageIds.Add(k_UguiPackageId);
        }
    }
#endif

    /// <summary>
    /// we implement a very simple version comparator, we don't want to implement semver
    /// so we truncate everything after the "-" in the version string
    /// </summary>
    /// <param name="semver"></param>
    /// <returns></returns>
    static Version GetTruncatedVersion(string semver)
    {
        var semverSuffixPos = semver.IndexOf("-");
        return new Version(semverSuffixPos > -1 ? semver.Substring(0, semverSuffixPos) : semver);
    }

    /// <summary>
    /// Checks if the packageId is a higher version or not present in current manifest
    /// </summary>
    /// <param name="packageId"></param>
    /// <param name="packageList"></param>
    /// <returns>Returns true if new package has higher version or is does not exist in manifest</returns>
    static bool HasHigherVersion(string packageId, ListRequest packageList)
    {
        var separatorPos = packageId.IndexOf("@");
        var newPackageName = packageId.Substring(0, separatorPos);
        var newPackageVersion = packageId.Substring(separatorPos + 1);

        var curPackageInfo = packageList.Result.FirstOrDefault(p => p.packageId.StartsWith(newPackageName));
        // Checks if package is already present and has higher version
        if (curPackageInfo != null)
        {
            var curVersion = GetTruncatedVersion(curPackageInfo.version);
            var newVersion = GetTruncatedVersion(newPackageVersion);
            if (newVersion.CompareTo(curVersion) <= 0)
            {
                return false;
            }
        }

        return true;
    }

    static void DeleteFolder(string directoryPath)
    {
        File.Delete(directoryPath + ".meta");
        Directory.Delete(directoryPath, recursive: true);
        AssetDatabase.Refresh();
    }

    [System.Serializable]
    public class JsonListWrapper<T>
    {
        public List<T> dependencies;
        public JsonListWrapper(List<T> dependencies) => this.dependencies = dependencies;
    }
}
