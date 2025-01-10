using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Gacha
{
    public class Probability
    {

        public static int[] GetItem()
        {
            int rand = new Random().Next(10);
            int find = 5;
            if (rand >= 0 || rand < 4)
            {
                find = 2;
            }
            if (rand >= 4 || rand < 7)
            {
                find = 3;
            }
            if (rand >= 7 || rand < 9)
            {
                find = 4;
            }
            List<int> pfps = new List<int>();
            List<int> backs = new List<int>();
            int index = 0;
            foreach (int x in Translator.pfpRarities)
            {
                if (x == find)
                {
                    pfps.Add(index);
                }
                index++;

            }

            index = 0;
            foreach (int x in Translator.backgroundRarities)
            {
                if (x == find)
                {
                    backs.Add(index);
                }
                index++;

            }

            switch (new Random().Next(2))
            {
                case 0:
                    return new int[] { 0, pfps[new Random().Next(pfps.Count - 1)] };

                case 1:
                    return new int[] { 1, backs[new Random().Next(backs.Count - 1)] };

            }
            return new int[] { 0, 0 };

        }
    }
}
