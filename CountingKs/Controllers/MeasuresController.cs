using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class MeasuresController : BaseApiController
    {
        public MeasuresController(ICountingKsRepository repo)
            : base(repo)
        {

        }

        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = Repo.GetMeasuresForFood(foodid)
                               .ToList()
                               .Select(m => ModelFactory.Create(m));

            return results;
        }

        public IHttpActionResult Get(int foodid, int id)
        {
            var result = Repo.GetMeasure(id);
            if (result == null)
                return NotFound();
            if (result.Food.Id != foodid)
                return NotFound();

            return Ok(ModelFactory.Create(result));
        }
    }
}
