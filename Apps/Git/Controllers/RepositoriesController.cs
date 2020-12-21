using SUS.HTTP;
using SUS.MvcFramework;

using Git.Services;
using Git.ViewModels.Repositores;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoryService repositoryService;

        public RepositoriesController( IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }
        public HttpResponse All()
        {
            var repositores = this.repositoryService.AllRepositores(this.GetUserId());

            return this.View(repositores);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/User/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (name.Length < 3 || name.Length > 10)
            {
                return this.Error(GlobalConstants.RepositoryNameLenghtError);
            }

            var repository = new CreteRepositoryInputModel
            {
                Name = name,
                OwnerId = this.GetUserId(),
                IsPublic = repositoryType == "Public"
            };

            var isCreate =  this.repositoryService.CreateRepository(repository);

            if (!isCreate)
            {
                return this.Error(GlobalConstants.RepositoryNameAvaivable);
            }

            return this.Redirect("All");
        }
    }
}
