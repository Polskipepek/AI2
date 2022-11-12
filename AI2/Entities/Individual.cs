using AI2.Infrastructure;
using Extensions.BitArrays;
using System.Collections;

namespace AI2.Entities {
    public class Individual {

        public Individual() {
            Genotype = BitArrayHelper.FromByteLE((byte)Rand.Random.Next(0, 128)).Skip(1);
        }

        public Individual(BitArray genotype) {
            Genotype = genotype;
        }

        public static List<Item> Items = new() {
            new Item("buty", 1f, 1f),
            new Item("kanapka", 0.1f, 0.2f),
            new Item("laptop", 1.7f, 2.5f),
            new Item("kurtka", 1f, 2),
            new Item("Spodnie", 0.5f, 1f),
            new Item("zegarek", 0.2f, 2f),
            new Item("termos", 0.5f, 0.4f)
        };

        public BitArray Genotype { get; set; }

        public (float masa, float wartosc) GetResults() {
            float masa = 0;
            float wartosc = 0;
            for (int i = 0; i < 7; i++) {
                wartosc += Genotype[i] ? Items[i].Wartosc : 0;
                masa += Genotype[i] ? Items[i].Masa : 0;
            }
            if (masa > 2.5) wartosc = 0;

            return (masa, wartosc);
        }
    }
}