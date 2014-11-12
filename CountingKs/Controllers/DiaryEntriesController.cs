using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class DiaryEntriesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiaryEntriesController(ICountingKsRepository repo, ICountingKsIdentityService identityService)
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryEntryModel> Get(DateTime diaryid)
        {
            var results = Repo.GetDiaryEntries(_identityService.CurrentUser, diaryid)
                                .ToList()
                                .Select(e => ModelFactory.Create(e));

            return results;
        }

        public IHttpActionResult Get(DateTime diaryid, int id)
        {
            var result = Repo.GetDiaryEntry(_identityService.CurrentUser, diaryid, id);

            if (result == null)
                return NotFound();

            return Ok(ModelFactory.Create(result));
        }

        public IHttpActionResult Post(DateTime diaryid, [FromBody]DiaryEntryModel model)
        {
            try
            {
                var entity = ModelFactory.Parse(model);
                if (entity == null) return BadRequest("Could not read diary entry in body");

                var diary = Repo.GetDiary(_identityService.CurrentUser, diaryid);
                if (diary == null) return NotFound();

                // Check for duplicate
                if (diary.Entries.Any(e => e.Measure.Id == entity.Measure.Id)) return BadRequest("Duplicate measure not allowed");

                // Save The New Entry
                diary.Entries.Add(entity);
                if(!Repo.SaveAll())
                {
                    // Save Fail
                    return BadRequest("Could not save entry");
                }

                var newModel = ModelFactory.Create(entity);
                return Created(newModel.Url, newModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
