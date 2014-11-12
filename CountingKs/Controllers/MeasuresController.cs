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
    [RoutePrefix("api/nutrition/foods/{foodid:int}")]
    public class MeasuresController : BaseApiController
    {
        public MeasuresController(ICountingKsRepository repo)
            : base(repo)
        {

        }

        [Route("measures", Name="Measures")]
        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = Repo.GetMeasuresForFood(foodid)
                               .ToList()
                               .Select(m => ModelFactory.Create(m));

            return results;
        }

        [Route("measures/{id:int}")]
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
