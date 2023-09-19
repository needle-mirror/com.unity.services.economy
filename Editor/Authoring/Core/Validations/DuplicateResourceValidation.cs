using System.Collections.Generic;
using System.Linq;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Core.Validations
{
    static class DuplicateResourceValidation
    {
        public static IReadOnlyList<IEconomyResource> FilterDuplicateResources(
            IReadOnlyList<IEconomyResource> resources,
            out IReadOnlyList<IGrouping<string, IEconomyResource>> duplicateGroups)
        {
            duplicateGroups = resources
                .GroupBy(r => r.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            var hashset = new HashSet<string>(duplicateGroups.Select(g => g.Key));

            return resources
                .Where(r => !hashset.Contains(r.Id))
                .ToList();
        }

        public static (string, string) GetDuplicateResourceErrorMessages(
            IEconomyResource targetEconomyResource,
            IReadOnlyList<IEconomyResource> group)
        {
            var duplicates = group.ToList()
                .FindAll(x => !string.Equals(x.Path, targetEconomyResource.Path));

            var duplicatesStr = string.Join(", ", duplicates.Select(d => $"'{d.Path}'"));
            var shortMessage = $"'{targetEconomyResource.Path}' was found duplicated in other files: {duplicatesStr}";
            var message = $"Multiple resources with the same identifier '{targetEconomyResource.Id}' were found. "
                          + "Only a single resource for a given identifier may be deployed/fetched at the same time. "
                          + "Give all resources unique identifiers or deploy/fetch them separately to proceed."
                          + shortMessage;
            return (shortMessage, message);
        }
    }
}
