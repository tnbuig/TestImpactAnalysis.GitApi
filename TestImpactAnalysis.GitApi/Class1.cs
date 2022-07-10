using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestImpactAnalysis.GitApi
{
    public class DiffReport
    {
        /// <summary>
        /// Changes to text files or configuration files or any non C# class.
        /// </summary>
        public bool IsOnlyNonClasses { get; set; }

        public bool IsOnlyTestsChanged { get; set; }
        IEnumerable<string> TestChangedFiles { get; set; }

        public bool IsOnlyRenaming { get; set; }
        IEnumerable<string> RenamedClasses { get; set; }

        public bool IsOnlyDeletion { get; set; }
        IEnumerable<string> DeletedClasses { get; set; }

        public bool IsOnlyCreation { get; set; }
        IEnumerable<string> CreatedClasses { get; set; }

        public bool IsModifiedClasses { get; set; }
        IEnumerable<string> ModifiedClasses { get; set; }

    }

    /// <summary>
    /// https://stackoverflow.com/questions/5721447/using-git-to-identify-all-modified-functions-in-a-revision/39439045#39439045
    /// https://stackoverflow.com/questions/53956170/git-get-method-function-names-of-the-file-in-which-changes-are-made
    /// https://blog.somewhatabstract.com/2015/06/22/getting-information-about-your-git-repository-with-c/
    /// </summary>
    public class Class1
    {
        public string GetListOfCommitsHistory(string repositoryPath)
        {
            StringBuilder result = new StringBuilder();

            var repo = new LibGit2Sharp.Repository(repositoryPath);
            foreach (Commit commit in repo.Commits)
            {
                foreach (var parent in commit.Parents)
                {
                    result.Append("{commit.Sha} | {commit.MessageShort}");
                    foreach (TreeEntryChanges change in repo.Diff.Compare<TreeChanges>(parent.Tree,
                    commit.Tree))
                    {
                        result.Append($"{change.Status} : {change.Path}");
                    }
                }
            }
            return result.ToString();
        }

        public bool IsRepositoryHavePendingChanges(string repositoryPath)
        {
            using (var repo = new Repository(repositoryPath))
            {
                RepositoryStatus status = repo.RetrieveStatus();
                return status.IsDirty;
            }
        }

        public IEnumerable<string> GetListOfPendingChanges(string repositoryPath)
        {
            List<string> result = new List<string>();
            using (var repo = new Repository(repositoryPath))
            {
                RepositoryStatus status = repo.RetrieveStatus();
                if (status.IsDirty)
                {
                    foreach (var s in status.Where(i=>i.State!= FileStatus.Ignored))
                    {
                        result.Add(s.FilePath);
                    }
                }
            }

            return result;
        }

        public IEnumerable<string> GetDiff(string repositoryPath)
        {
            using (var repo = new Repository(repositoryPath))
            {
                var result = repo.Diff.Compare<TreeChanges>();
                List<TreeEntryChanges> t = result.Modified.ToList();
                List<TreeEntryChanges> tt = result.Added.ToList();


                return null;
            }
        }
    }
}
