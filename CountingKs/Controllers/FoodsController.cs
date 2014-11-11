using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CountingKs.Controllers
{
    [RoutePrefix("api/nutrition/foods")]
    public class FoodsController : BaseApiController
    {
        public FoodsController(ICountingKsRepository repo) 
            : base(repo)
        {

        }
        [Route("", Name="Food")]
        public IEnumerable<FoodModel> Get(bool includeMeasures = true)
        {
            IQueryable<Food> query;

            if (includeMeasures)
            {
                query = Repo.GetAllFoodsWithMeasures();
            }
            else
            {
                query = Repo.GetAllFoods();
            }

            var results = query.OrderBy(f => f.Description)
                                .Take(25)
                                .ToList()
                                .Select(f => ModelFactory.Create(f));
            return results;
        }

        [Route("{foodid:int}")]
        public FoodModel Get(int foodid)
        {
            return ModelFactory.Create(Repo.GetFood(foodid));
        }
    }
}
