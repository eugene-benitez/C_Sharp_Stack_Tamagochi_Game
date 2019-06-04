using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dojodachi.Models
{
    public class Puppy
    {
        public string Name = "Pikachu";

        public int Happiness = 20;

        public int Fullness = 20;

        public int Energy = 50;

        public int Meals = 3;

        public bool isDead = false;

        public string Log = "Hello! My name is Pikachu! Can you play with me?";

        public Puppy Feed()
        {
            var y = new Random();
            int yuck = y.Next(1, 5);

            if (Meals < 1)
            {
                Log = "No more food. I need to work for it!";
                return this;
            }

            if (yuck == 1)
            {
                Meals -= 1;
                Log = "Yuck! Meals - 1";
                return this;
            }

            else
            {
                var random = new Random();
                Meals -= 1;
                int full = random.Next(5, 11);
                Fullness += full;
                Log = $"Yum! Fullness + {full} | Meals - 1";
                return this;

            }
        }

        public Puppy Play()
        {
            var y = new Random();
            int yuck = y.Next(1, 5);

            if (Energy < 1)
            {
                Log = $"No more energy! I must get some sleep.";
                return this;
            }
            if (yuck == 1)
            {
                Energy -= 5;
                Log = "Yuck! Energy - 5";
                return this;
            }

            else
            {
                var random = new Random();
                Energy -= 5;
                int h = random.Next(5, 11);
                Happiness += h;
                Log = $"Fun !! Happiness + {h} | Energy -5";
                return this;
            }
        }

        public Puppy Work()
        {
            if (Energy < 1)
            {
                Log = $"No more energy! I must get some sleep.";
                return this;
            }
            var random = new Random();
            Energy -= 5;
            int m = random.Next(1, 4);
            Meals += m;
            Log = $"I found some food !! Meals + {m} | Energy - 5";
            return this;
        }

        public Puppy Sleep()
        {
            var random = new Random();
            Energy += 15;
            Happiness -= 5;
            Fullness -= 5;
            Log = $"I got some good sleep !! Energy + 15 | Happiness - 5 | Fullness - 5";
            return this;
        }
    }

}
