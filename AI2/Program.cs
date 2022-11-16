// See https://aka.ms/new-console-template for more information
using AI2.Crossover;
using AI2.Entities;
using AI2.GeneticAlgorithm;
using AI2.Mutation;
using AI2.ParentSelection;
using AI2.PopulationSelection;
using Extensions.BitArrays;
using System.Collections;


Console.WriteLine("Hello, World!");
Individual individual = new();
var res = individual.GetResults();

//Console.WriteLine($"masa: {res.masa.ToString("0.0")}, wartosc: {res.wartosc.ToString("0.0")}");
//Console.WriteLine($"Max wartosc: {bruteforce(out var bestArray)}, dla {bestArray.ToBitString()}");

ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.05f), new RouletteWheelParentSelector(0.5f), new FitnessPopulationSelection(), new SinglePointCrossover(.5f), 10);
for (int i = 0; i < 100; i++) {
    classic.Update();
    Console.WriteLine($"Iter: {(i + 1):00}\t{classic}");
}

Console.ReadLine();

float bruteforce(out BitArray bestArray) {
    bestArray = null;

    float maxWartosc = 0;
    for (byte i = 0; i < 128; i++) {
        var ba = BitArrayHelper.FromByteLE(i).Skip(1);

        Individual ind = new(ba);
        var result = ind.GetResults();
        Console.WriteLine($"Dla {ind.Genotype.ToBitString()} wartosc to {result.wartosc} a masa to {result.masa}");

        if (result.wartosc > maxWartosc && result.masa <= 2.5f) {
            maxWartosc = result.wartosc;
            bestArray = ind.Genotype;
        }
    }
    return maxWartosc;
}