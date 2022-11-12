using AI2.Crossover;
using AI2.Entities;
using AI2.Mutation;
using AI2.ParentSelection;
using AI2.PopulationSelection;

namespace AI2.GeneticAlgorithm {
    internal class ClassicGeneticAlgorithm {
        public ClassicGeneticAlgorithm(IMutation mutationStrategy, IParentSelection parentSelectionStrategy, IPopulationSelection populationSelectionStrategy, ICrossover crossoverStrategy, int populationSize) {
            this.mutationStrategy = mutationStrategy;
            this.parentSelectionStrategy = parentSelectionStrategy;
            this.populationSelectionStrategy = populationSelectionStrategy;
            this.crossoverStrategy = crossoverStrategy;
            this.populationSize = populationSize;
            population = Enumerable.Range(0, populationSize).Select(x => new Individual()).ToList();
        }

        public IEnumerable<Individual> Population => population;

        private readonly IMutation mutationStrategy;
        private readonly IParentSelection parentSelectionStrategy;
        private readonly IPopulationSelection populationSelectionStrategy;
        private readonly ICrossover crossoverStrategy;

        private readonly int populationSize;
        private List<Individual> population;

        public void Update() {
            var parents = parentSelectionStrategy.GetParents(Population);
            var newIndividuals = crossoverStrategy.Crossover(Population, parents);
            var newPopulation = populationSelectionStrategy.GetNextPopulation(population, newIndividuals);
            population = mutationStrategy.Mutate(newPopulation).ToList();
        }

        private float GetAveragePopulationFitness(out float minFitness, out float maxFitness) {
            var fitnesses = population.Select(individual => individual.GetResults().wartosc);

            maxFitness = fitnesses.Max();
            minFitness = fitnesses.Min();

            return fitnesses.Average();
        }

        public override string ToString() {
            return $"Fitness: {GetAveragePopulationFitness(out var minFitness, out var maxFitness):0.0}\tMinFitness: {minFitness:0.0}\tMaxFitness: {maxFitness:0.0}\tCount: {population.Count}";
        }
    }
}
