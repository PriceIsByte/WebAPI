using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class FoodsController : BaseApiController
    {
        public FoodsController(ICountingKsRepository repo) 
            : base(repo)
        {

        }

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

        public FoodModel Get(int foodid)
        {
            return ModelFactory.Create(Repo.GetFood(foodid));
        }
    }
}
