namespace AI2.ParentSelection {
    class RouletteWheel {

        private readonly float[] probabilities;
        private readonly Random random;

        public RouletteWheel(float[] probabilities) {
            this.probabilities = probabilities;
            random = new Random();
        }

        public int Spin() {
            var rand = random.NextDouble();
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
