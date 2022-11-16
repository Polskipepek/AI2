using AI2.Infrastructure;

namespace AI2.ParentSelection {
    public class Tournament {

        private readonly float[] probabilities;

        public Tournament(float[] probabilities) {
            this.probabilities = probabilities;
        }

        public int GetResult() {
            var rand = Rand.Random.NextDouble() * probabilities.Sum();
            float currentVal = 0;
            int i = 0;

            while (currentVal < rand) {
                currentVal += probabilities[i];

                if (currentVal >= rand)
                    break;
                i++;
            }

            return i;
        }
    }
}
