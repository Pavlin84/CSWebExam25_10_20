using Git.ViewModels.Repositores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoryService
    {
        public ICollection<AllRepositoresVewModel> AllRepositores(string userId);

        public bool CreateRepository(CreteRepositoryInputModel repositoryInputModel);

    }
}
