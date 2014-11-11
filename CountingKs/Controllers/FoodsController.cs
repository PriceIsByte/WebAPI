using CountingKs.Data;
using CountingKs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository _repo;
        private readonly ModelFactory _modelFactory;

        public FoodsController(ICountingKsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<FoodModel> Get()
        {
            var result = _repo.GetAllFoodsWithMeasures()
                                .OrderBy(f => f.Description)
                                .Take(25)
                                .ToList()
                                .Select(f => _modelFactory.Create(f));
            return result;
        }

        public FoodModel Get(int id)
        {
            return _modelFactory.Create(_repo.GetFood(id));
        }
    }
}
