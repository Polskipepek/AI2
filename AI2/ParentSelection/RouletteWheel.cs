using AI2.Infrastructure;

namespace AI2.ParentSelection {
    public class RouletteWheel {

        private readonly float[] probabilities;

        public RouletteWheel(float[] probabilities) {
            this.probabilities = probabilities;
        }

        public int Spin() {
            var rand = Rand.Random.NextDouble();
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
