using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneticsTester : MonoBehaviour
{
    public Gene.Trait[] CurrentTraits = new Gene.Trait[55];

    private GeneSystem geneSystem;
    private string genomesString;
    private string sensationsString;

    private int attempt = 0;

    // Start is called before the first frame update
    void Start()
    {
        geneSystem = new GeneSystem();

        var traitsCount = Enum.GetNames(typeof(Gene.Trait)).Length;

        for (int i = 0; i < 55; i++)
        {
            CurrentTraits[i] = Gene.Trait.Unknown;
        }

        for (int i = 0; i < traitsCount; i++)
        {
            CurrentTraits[i] = (Gene.Trait) i;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    public string MakeRandomGenes(int highChance)
    {
        string genome = "";

        for (int i = 1; i <= 55; i++)
        {
            if (Random.Range(0, highChance) == 0)
            {
                string block = "000";

                switch (Random.Range(0, 3))
                {
                    case 0:
                        block = "DAC";
                        break;
                    case 1:
                        block = "BEA";
                        break;
                    case 2:
                        block = "802";
                        break;
                }

                genome += $"{i}{block}";
            }
            else
            {
                genome += $"{i}000";
            }
        }

        return genome;
    }

    public string GetSymptoms(string genomeString)
    {
        string symptomsString = "";

        int genomeIndex = 0;

        for (int i = 0; i < 55; i++)
        {
            int indexWidth = (i < 9) ? 1 : 2;

            string genomeHex = genomeString.Substring(genomeIndex + indexWidth, 3);
            int value = int.Parse(genomeHex, System.Globalization.NumberStyles.HexNumber);
            int treshold = Gene.GetTraitTreshold(CurrentTraits[i]);
            
            genomeIndex += (indexWidth + 3);

            if (value >= treshold)
            {
                //Simulate the random chance to activate some power genes
                //I don't know exactly how this works but this will do. It's not that important for the tester.
                if (Gene.IsRandomChanceTrait(CurrentTraits[i]) && Random.Range(0, 100) > 15)
                {
                    continue;
                }
                
                symptomsString += $"{Gene.GetSymptomForTrait(CurrentTraits[i])}\n";
            }
        }

        return symptomsString;
    }

    public void Test()
    {
        genomesString = MakeRandomGenes(4);
        sensationsString = GetSymptoms(genomesString);

        geneSystem.Apply(genomesString, sensationsString);
        attempt++;
        Debug.Log(attempt);
    }

    void OnGUI()
    {
        {
            float x = 50;
            float y = 20;

            float width = 200;
            float height = 15;

            if (GUI.Button(new Rect(x, y, width, 20), "Random genes test"))
            {
                Test();
            }
        }
        
        var genes = geneSystem.Genes;

        Genetics.DrawActiveGenomes(genes);
        Genetics.DrawGenesToApply(genes, genomesString);
        Genetics.DrawSenations(sensationsString);
        Genetics.DrawGeneInfo(genes);
    }
}