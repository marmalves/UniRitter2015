using System.Collections.Generic;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class CommentsController : BaseController<CommentModel>
    {
        public CommentsController(IRepository<CommentModel> repo)
            : base(repo)
        {

        }
    }
}