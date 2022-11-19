using AI2.Infrastructure;
using Extensions.BitArrays;
using System.Collections;

namespace AI2.Entities {
    public class Individual {

        public Individual(int length = 7) {
            Genotype = BitArrayHelper.FromByteLE((byte)Rand.Random.Next(0, (int)Math.Pow(2, length))).Skip(1);
        }

        public Individual(BitArray genotype) {
            Genotype = genotype;
        }

        public static List<Item> Items { get; set; }

        public static void SetItems(IEnumerable<Item> items) {
            Items = items.ToList();
        }

        public BitArray Genotype { get; set; }

        public (float masa, float wartosc) GetResults() {
            float masa = 0;
            float wartosc = 0;
            for (int i = 0; i < Items.Count - 1; i++) {
                wartosc += Genotype[i] ? Items[i].Wartosc : 0;
                masa += Genotype[i] ? Items[i].Masa : 0;
            }
            if (masa > 2.5) wartosc = 0;

            return (masa, wartosc);
        }
    }
}