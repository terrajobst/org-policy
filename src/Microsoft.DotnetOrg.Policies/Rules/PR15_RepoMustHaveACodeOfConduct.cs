using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.DotnetOrg.GitHubCaching;

namespace Microsoft.DotnetOrg.Policies.Rules
{
    internal sealed class PR15_RepoMustHaveACodeOfConduct : PolicyRule
    {
        public static PolicyDescriptor Descriptor { get; } = new PolicyDescriptor(
            "PR15",
            "Repo must have a Code of Conduct",
            PolicySeverity.Error
        );

        public override async Task GetViolationsAsync(PolicyAnalysisContext context)
        {
            // This rule is not scoped to anyone because both Microsoft and .NET Foundation
            // projects are expected to follow this policy.

            var client = await GitHubClientFactory.CreateAsync();

            foreach (var repo in context.Org.Repos)
            {
                if (repo.IsPrivate || repo.IsArchived)
                    continue;

                var coc = await client.GetCodeOfConduct(repo.Org.Name, repo.Name);
                if (coc != null)
                    continue;

                context.ReportViolation(
                    Descriptor,
                    $"Repo '{repo.Name}' must have a Code of Conduct",
                    $@"
                        The repo {repo.Markdown()} needs to include a file that links to the Code of Conduct.

                        For more details, see [PR15](https://github.com/dotnet/org-policy/blob/master/doc/PR15.md).
                    ",
                    repo: repo
                );
            }
        }
    }
}
