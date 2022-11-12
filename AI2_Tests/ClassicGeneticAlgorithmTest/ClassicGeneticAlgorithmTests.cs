namespace AI2_UnitTests.ClassicGeneticAlgorithmTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void ClassicGeneticAlgorithm_MinFitnessOtherThanZero() {
            //Arrange
            ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.5f), new FIFOPopulationSelector(), new SinglePointCrossover(0.5f), 10);

            //Act
            for (int i = 0; i < 100; i++) {
                classic.Update();
                Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
            }

            //Assert
            classic.GetAveragePopulationFitness(out var minFitenss, out _);
            Assert.AreNotEqual(0, minFitenss);
        }

        [TestMethod]
        public void ClassicGeneticAlgorithm_MaxFitnessOptimal() {
            //Arrange
            ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.5f), new FIFOPopulationSelector(), new SinglePointCrossover(0.5f), 10);

            //Act
            for (int i = 0; i < 100; i++) {
                classic.Update();
                Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
            }

            //Assert
            classic.GetAveragePopulationFitness(out _, out var maxFitness);
            Assert.AreEqual(5.6f, maxFitness);
        }
    }
}
