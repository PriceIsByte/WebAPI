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
    public class MeasuresController : ApiController
    {
        private readonly ICountingKsRepository _repo;
        private readonly ModelFactory _modelFactory;

        public MeasuresController(ICountingKsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = _repo.GetMeasuresForFood(foodid)
                               .ToList()
                               .Select(m => _modelFactory.Create(m));

            return results;
        }

        public MeasureModel Get(int foodid, int id)
        {
            var result = _repo.GetMeasure(id);
            if (result == null)
                return null;

            return result.Food.Id == foodid ? _modelFactory.Create(result) : null;
        }
    }
}
