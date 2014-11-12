using CountingKs.Data;
using CountingKs.Data.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace CountingKs.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly ICountingKsRepository _repo;

        public ModelFactory(HttpRequestMessage request, ICountingKsRepository repo)
        {
            _urlHelper = new UrlHelper(request);
            _repo = repo;
        }

        public FoodModel Create(Food food)
        {
            return new FoodModel
            {
                URL = _urlHelper.Link("Food", new { foodid = food.Id }),
                Description = food.Description,
                Measures = food.Measures.Select(m => Create(m))
            };
        }

        public MeasureModel Create(Measure measure)
        {
            return new MeasureModel
            {
                Url = _urlHelper.Link("Measures", new { foodid = measure.Food.Id, id = measure.Id }),
                Description = measure.Description,
                Calories = Math.Round(measure.Calories)
            };
        }

        public DiaryModel Create(Diary diary)
        {
            return new DiaryModel
            {
                Url = _urlHelper.Link("Diaries", new { diaryid = diary.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate = diary.CurrentDate,
                Entires = diary.Entries.Select(e => Create(e))
            };
        }

        public DiaryEntryModel Create(DiaryEntry diaryEntry)
        {
            return new DiaryEntryModel
            {
                Url = _urlHelper.Link("DiaryEntries", new { diaryid = diaryEntry.Diary.CurrentDate.ToString("yyyy-MM-dd"), id = diaryEntry.Id}),
                FoodDescription = diaryEntry.FoodItem.Description,
                measureDescription = diaryEntry.Measure.Description,
                measureUrl = _urlHelper.Link("Measures", new { foodid = diaryEntry.FoodItem.Id, id = diaryEntry.Measure.Id }),
                Quantity = diaryEntry.Quantity
            };
        }

        public DiaryEntry Parse(DiaryEntryModel model)
        {
            try
            {
                var entry = new DiaryEntry();

                if(model.Quantity != default(double))
                {
                    entry.Quantity = model.Quantity;
                }

                var uri = new Uri(model.measureUrl);
                var measureId = int.Parse(uri.Segments.Last());
                var measure = _repo.GetMeasure(measureId);

                entry.Measure = measure;
                entry.FoodItem = measure.Food;

                return entry;
            }
            catch
            {
                return null;
            }
        }
    }
}
