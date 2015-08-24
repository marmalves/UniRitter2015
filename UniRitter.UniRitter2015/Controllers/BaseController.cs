using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    abstract public class BaseController<TModel> : ApiController
        where TModel : class, IModel
    {
        private readonly IRepository<TModel> repo;

        public BaseController(IRepository<TModel> repo)
        {
            this.repo = repo;
        }

        // GET: TModel
        public async Task<IHttpActionResult> Get()
        {
            return Json(await repo.GetAll());
        }

        // GET: TModel/id
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var data = await repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }

            return NotFound();
        }

        // POST: TModel
        public async Task<IHttpActionResult> Post([FromBody] TModel item)
        {
            if (ModelState.IsValid)
            {
                var data = await repo.Add(item);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: TModel/id
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] TModel item)
        {
            var data = await repo.Update(id, item);
            return Json(item);
        }

        // DELETE: TModel/id
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}