namespace AI2_UnitTests.ClassicGeneticAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ClassicGeneticAlgorithm_MinFitnessOtherThanZero()
        {
            //Arrange
            ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.05f), new FIFOPopulationSelector(), new SinglePointCrossover(0.05f), 10);

            //Act
            for (int i = 0; i < 100; i++)
            {
                classic.Update();
                Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
            }

            //Assert
            classic.GetAveragePopulationFitness(out var minFitenss, out _);
            Assert.AreNotEqual(0, minFitenss);
        }

        [TestMethod]
        public void ClassicGeneticAlgorithm_MaxFitnessReached()
        {
            //Arrange
            ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.05f), new FIFOPopulationSelector(), new SinglePointCrossover(0.05f), 10);

            //Act
            for (int i = 0; i < 100; i++)
            {
                classic.Update();
                Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
            }

            //Assert
            classic.GetAveragePopulationFitness(out _, out var maxFitness);
            Assert.AreEqual(5.6f, maxFitness);
        }

        [TestMethod]
        public void ClassicGeneticAlgorithm_FitnessLargeThan5()
        {
            //Arrange 
            ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.05f), new FIFOPopulationSelector(), new SinglePointCrossover(0.05f), 100);

            //Act
            for (int i = 0; i < 100; i++)
            {
                classic.Update();
                Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
            }

            //Assert
            float fitness = classic.GetAveragePopulationFitness(out var minFitness, out var maxFitness);
            Assert.IsTrue(fitness > 5);
        }
    }
}
