using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository _repo;

        public FoodsController(ICountingKsRepository repo)
        {
            _repo = repo;
        }

        public object Get()
        {
            var result = _repo.GetAllFoods()
                                .OrderBy(f => f.Description)
                                .Take(25)
                                .ToList();
            return result;
        }
    }
}
