using System;
using System.Linq;
using System.Collections.Generic;

using Git.Data;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public class CommitService : ICommitService
    {
        private readonly ApplicationDbContext db;

        public CommitService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<CommitViewModel> AllCommits(string userId)
        {
            var commits = db.Commits
                 .Where(x => x.CreatorId == userId && !x.IsDeleted)
                 .OrderBy(x => x.CreatedOn)
                 .Select(x => new CommitViewModel
                 {
                     Id = x.Id,
                     Descripion = x.Description,
                     RepositoryName = x.Repository.Name,
                     CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm")
                 }).ToList();

            return commits;
        }

        public void CreteCommit(CommitInputModel commit)
        {
            db.Commits.Add(new Commit
            {
                Description = commit.Descripion,
                CreatorId = commit.CreatorId,
                RepositoryId = commit.RepositoryId,
                CreatedOn = DateTime.UtcNow
            });

            db.SaveChanges();
        }

        public bool DeleteCommit(string commitId, string userId)
        {
            var commitForDelete = this.db.Commits.FirstOrDefault(x => x.Id == commitId && x.CreatorId == userId);

            if (commitForDelete == null)
            {
                return false;
            }

            this.db.Remove(commitForDelete);
            this.db.SaveChanges();

            return true;
        }

        public string GetRepository(string repotoryId)
        {
            var repository = this.db.Repositories.FirstOrDefault(x => x.Id == repotoryId);
            if (repository == null)
            {
                return null;
            }

            return repository.Name;

        }
    }
}
