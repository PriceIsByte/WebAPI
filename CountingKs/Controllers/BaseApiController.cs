using CountingKs.Data;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly ICountingKsRepository _repo;
        private ModelFactory _modelFactory;

        public BaseApiController(ICountingKsRepository repo)
        {
            _repo = repo;
        }

        protected ICountingKsRepository Repo
        {
            get { return _repo; }
        }

        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request);
                }
                return _modelFactory;
            }
        }
    }
}
