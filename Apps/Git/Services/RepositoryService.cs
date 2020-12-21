using System;
using System.Linq;
using System.Collections.Generic;

using Git.Data;
using Git.ViewModels.Repositores;

namespace Git.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationDbContext db;
        public RepositoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<AllRepositoresVewModel> AllRepositores(string userId)
        {
            var repositores = db.Repositories
                .Where(x => x.IsPublic)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new AllRepositoresVewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    ComitsCount = x.Commits.Where(y => !y.IsDeleted).Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    IsPublic = true,
                })
                .ToList();
            if (userId != null)
            {
                var yourPrivateRepositores = db.Repositories
                .Where(x => x.OwnerId == userId && !x.IsPublic)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new AllRepositoresVewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    ComitsCount = x.Commits.Where(y => !y.IsDeleted).Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    IsPublic = false
                }).ToList();

                repositores.AddRange(yourPrivateRepositores);
            }

            return repositores;

        }

        public bool CreateRepository(CreteRepositoryInputModel repositoryInputModel)
        {
            if (db.Repositories.Any(x => x.Name == repositoryInputModel.Name))
            {
                return false;
            }

            db.Repositories
                 .Add(new Repository
                 {
                     Name = repositoryInputModel.Name,
                     CreatedOn = DateTime.UtcNow,
                     IsPublic = repositoryInputModel.IsPublic,
                     OwnerId = repositoryInputModel.OwnerId
                 });

            db.SaveChanges();
            return true;
        }
    }
}
