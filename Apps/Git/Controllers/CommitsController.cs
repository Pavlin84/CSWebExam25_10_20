using Git.Services;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositores;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitService commitService;

        public CommitsController(ICommitService commitService)
        {
            this.commitService = commitService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var commits = this.commitService.AllCommits(this.GetUserId());

            return this.View(commits);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View(new RepositoryForCommitViewModel 
            {
                Id = id,
                Name = this.commitService.GetRepository(id)
            });
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (description.Length < 5)
            {
                return this.Error(GlobalConstants.DescriptionLenghtError);
            }

            this.commitService.CreteCommit(new CommitInputModel
            {
                Descripion = description,
                RepositoryId = id,
                CreatorId = this.GetUserId()
            });

            return this.Redirect("/Repositories/All");

        }
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var isDeleted = this.commitService.DeleteCommit(id, this.GetUserId());

            if (!isDeleted)
            {
                return this.Error(GlobalConstants.DeleteError);
            }
            return this.Redirect("All");
        }
    }
}
