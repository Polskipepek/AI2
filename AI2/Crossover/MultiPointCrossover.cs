using AI2.Entities;
using AI2.Infrastructure;
using Extensions.BitArrays;
using System.Collections;

namespace AI2.Crossover {
    public class MultiPointCrossover : ICrossover {

        public MultiPointCrossover(float probability) {
            Probability = probability;
        }

        public float Probability { get; }

        public IEnumerable<Individual> Crossover(IEnumerable<Individual> individuals, IEnumerable<Individual> parents) {
            for (int i = 0; i < parents.Count() - 1; i += 2) {
                if (TryCrossover(individuals, parents.ElementAt(i), parents.ElementAt(i + 1), out var newGenes)) {
                    yield return new Individual(newGenes);
                } else {
                    var parent = Rand.Random.Next(0, 1) == 0 ? parents.ElementAt(i) : parents.ElementAt(i + 1);
                    yield return parent;
                }
            }
        }

        bool TryCrossover(IEnumerable<Individual> individuals, Individual parentA, Individual parentB, out BitArray newGenes) {
            newGenes = null;

            if (Rand.Random.NextDouble() > Probability)
                return false;

            int currentId = 0;
            int genotypeLength = parentA.Genotype.Length;

            while (currentId < genotypeLength) {
                var currentLength = Rand.Random.Next(1, genotypeLength - currentId);
                var crossed = SmallCrossover(parentA, parentB, currentLength, currentId);

                if (newGenes == null)
                    newGenes = new BitArray(crossed);
                else
                    newGenes = newGenes.MergeWith(crossed);

                currentId += currentLength;
            }
            return true;
        }

        BitArray SmallCrossover(Individual parentA, Individual parentB, int length, int startIndex) {
            var parent = Rand.Random.Next(0, 1) == 0 ? parentA : parentB;

            return parent.Genotype.Skip(startIndex).Take(length);
        }
    }
}
