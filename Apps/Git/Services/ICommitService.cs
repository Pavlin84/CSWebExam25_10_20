using System.Collections.Generic;

using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface ICommitService
    {
        public void CreteCommit(CommitInputModel commit);

        public ICollection<CommitViewModel> AllCommits(string userId);

        public bool DeleteCommit(string commitId, string userId);

        public string GetRepository(string repotoryId);
    }
}
