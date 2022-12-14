// See https://aka.ms/new-console-template for more information
using AI2.Crossover;
using AI2.Entities;
using AI2.GeneticAlgorithm;
using AI2.Mutation;
using AI2.ParentSelection;
using AI2.PopulationSelection;
using Extensions.BitArrays;
using System.Collections;


Console.WriteLine("Cwiczenie 2!");
Console.WriteLine();

var defaultItems = new List<Item>() {
                new Item("buty", 1f, 1f),
                new Item("kanapka", 0.1f, 0.2f),
                new Item("laptop", 1.7f, 2.5f),
                new Item("kurtka", 1f, 2),
                new Item("Spodnie", 0.5f, 1f),
                new Item("zegarek", 0.2f, 2f),
                new Item("termos", 0.5f, 0.4f)
            };
Individual.SetItems(defaultItems);

Individual individual = new();
var res = individual.GetResults();

//Console.WriteLine($"masa: {res.masa.ToString("0.0")}, wartosc: {res.wartosc.ToString("0.0")}");
//Console.WriteLine($"Max wartosc: {bruteforce(out var bestArray)}, dla {bestArray.ToBitString()}");


ClassicGeneticAlgorithm classic = new(new BinaryMutation(0.1f), new StochasticUniversalSamplingParentSelection(8), new FIFOPopulationSelector(), new SinglePointCrossover(.1f), 16);
for (int i = 0; i < 101; i++) {
    classic.Update();
    if (i % 5 == 0)
        Console.WriteLine($"Iter: {(i):000}\t{classic}");
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