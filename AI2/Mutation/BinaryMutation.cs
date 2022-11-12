using AI2.Entities;
using AI2.Infrastructure;
using Extensions.BitArrays;

namespace AI2.Mutation {
    public class BinaryMutation : IMutation {

        public BinaryMutation(float mutationProb) {
            this.mutationProb = mutationProb;
        }

        private readonly float mutationProb;

        public IEnumerable<Individual> Mutate(IEnumerable<Individual> population) {
            foreach (var ind in population) {
                TryMutate(ind);
            }
            return population;
        }

        public void TryMutate(Individual individual) {

            if (Rand.Random.NextDouble() > mutationProb) {
                return;
            }

            var genotypeLength = 7;
            int bitFlipPosition = Rand.Random.Next(0, genotypeLength);
            byte mask = (byte)(1 << bitFlipPosition);
            var maskBa = BitArrayHelper.FromByteLE(mask).Skip(1);

            individual.Genotype = individual.Genotype.Xor(maskBa);
        }
    }
}
