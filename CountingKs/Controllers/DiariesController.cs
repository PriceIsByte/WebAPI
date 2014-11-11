using CountingKs.Data;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class DiariesController : BaseApiController
    {
        private readonly ICountingKsIdentityService _identityService;

        public DiariesController(ICountingKsRepository repo, ICountingKsIdentityService identityService)
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            var results = Repo.GetDiaries(_identityService.CurrentUser)
                              .OrderBy(d => d.CurrentDate)
                              .Take(10)
                              .ToList()
                              .Select(d => ModelFactory.Create(d));

            return results;
        }
    }
}
