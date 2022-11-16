using AI2.Entities;
using AI2.Infrastructure;
using Extensions.BitArrays;
using System.Collections;

namespace AI2.Crossover {
    public class SinglePointCrossover : ICrossover {

        public SinglePointCrossover(float probability) {
            Probability = probability;
        }

        public float Probability { get; }

        public IEnumerable<Individual> Crossover(IEnumerable<Individual> _, IEnumerable<Individual> parents) {
            for (int i = 0; i < parents.Count() - 1; i += 2) {
                if (TryCrossover(parents.ElementAt(i), parents.ElementAt(i + 1), out var newGeneA, out var newGeneB)) {
                    yield return new Individual(newGeneA.MergeWith(newGeneB));
                } else {
                    var parent = Rand.Random.Next(0, 1) == 0 ? parents.ElementAt(i) : parents.ElementAt(i + 1);
                    yield return parent;
                }
            }
        }

        private bool TryCrossover(Individual parentA, Individual parentB, out BitArray newGeneA, out BitArray newGeneB) {
            newGeneA = null;
            newGeneB = null;

            if (Rand.Random.NextDouble() > Probability)
                return false;

            var genotypeLength = parentA.Genotype.Length;
            int cut = Rand.Random.Next(1, genotypeLength);

            newGeneA = parentA.Genotype.Take(cut);
            newGeneB = parentB.Genotype.Skip(cut);

            return true;
        }
    }
}
