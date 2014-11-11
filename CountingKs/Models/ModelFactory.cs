﻿using CountingKs.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingKs.Models
{
    public class ModelFactory
    {
        public FoodModel Create(Food food)
        {
            return new FoodModel
            {
                Description = food.Description,
                Measures = food.Measures.Select(m => Create(m))
            };
        }

        public MeasureModel Create(Measure m)
        {
            return new MeasureModel
            {
                Description = m.Description,
                Calories = m.Calories
            };
        }
    }
}
