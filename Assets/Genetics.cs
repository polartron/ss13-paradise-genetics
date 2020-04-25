using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Gene
{
    public enum Trait
    {
        Cryokinesis,
        EmpatheticThought,
        Hulk,
        Jumpy,
        MatterEater,
        Dwarf,
        NoBreathing,
        NoPrints,
        PsyResist,
        Telekinesis,

        Chameleon,
        CloakofDarkness,
        ColdResistance,
        HighPressureIntestines,
        Morphism,
        Polymorphism,
        Regenerate,
        RemoteViewing,
        Telepathy,
        XrayVision,
        SpeedBoost,

        HeatResistance,
        ShockImmunity,
        Sober,
        Strong,

        Blindness,
        Chav,
        Clumsiness,
        ColorBlindness,
        Comic,
        Coughing,
        Deafness,
        Dizzy,
        Epilepsy,
        Hallucinate,
        Horns,
        IncendiaryMitochondria,
        Lisp,
        Loud,
        Mute,
        Nearsightedness,
        Nervousness,
        Obesity,
        Radioactive,
        Swedish,
        Tourettes,
        ToxicFarts,
        Unintelligible,
        GreyVocabulary,

        Unknown
    }

    public static Dictionary<string, Trait> Sensations = new Dictionary<string, Trait>()
    {
        {"You notice a strange cold tingle in your fingertips", Trait.Cryokinesis},
        {"You suddenly notice more about others than you did before", Trait.EmpatheticThought},
        {"Your muscles hurt", Trait.Hulk},
        {"Your leg muscles feel taut and strong", Trait.Jumpy},
        {"You feel hungry", Trait.MatterEater},
        {"Everything around you seems bigger now", Trait.Dwarf},
        {"You feel no need to breathe", Trait.NoBreathing},
        {"Your fingers feel numb", Trait.NoPrints},
        {"Your mind feels closed", Trait.PsyResist},
        {"You feel smarter", Trait.Telekinesis},
        {"You feel one with your surroundings", Trait.Chameleon},
        {"You begin to fade into the shadows", Trait.CloakofDarkness},
        {"Your body is filled with warmth", Trait.ColdResistance},
        {"You feel bloated and gassy", Trait.HighPressureIntestines},
        {"Your body feels if can alter its appearance", Trait.Morphism},
        {"You don't feel entirely like yourself somehow", Trait.Polymorphism},
        {"Your wounds start healing", Trait.Regenerate},
        {"Your mind can see things from afar", Trait.RemoteViewing},
        {"You feel you can project your thoughts", Trait.Telepathy},
        {"The walls suddenly disappear", Trait.XrayVision},
        {"You feel swift and unencumbered", Trait.SpeedBoost},
        {"Your skin is icy to the touch", Trait.HeatResistance},
        {"Your skin feels dry and unreactive", Trait.ShockImmunity},
        {"You feel unusually sober", Trait.Sober},
        {"You feel buff", Trait.Strong},
        {"You can't seem to see anything", Trait.Blindness},
        {"Ye feel like a reet prat like, innit", Trait.Chav},
        {"You feel lightheaded", Trait.Clumsiness},
        {"You feel a strange prickling in your eyes as your perception changes", Trait.ColorBlindness},
        {"Uh oh", Trait.Comic},
        {"You start coughing", Trait.Coughing},
        {"It's kinda quiet", Trait.Deafness},
        {"You feel very dizzy", Trait.Dizzy},
        {"You get a headache", Trait.Epilepsy},
        {"Your mind says", Trait.Hallucinate},
        {"A pair of horns erupt from your head", Trait.Horns},
        {"You suddenly feel rather hot", Trait.IncendiaryMitochondria},
        {"Thomething doethn't feel right", Trait.Lisp},
        {"YOU FEEL LIKE YELLING", Trait.Loud},
        {"You feel unable to express yourself at all", Trait.Mute},
        {"Your eyes feel weird", Trait.Nearsightedness},
        {"You feel nervous", Trait.Nervousness},
        {"You feel blubbery and lethargic", Trait.Obesity},
        {"You feel a strange sickness permeate your whole body", Trait.Radioactive},
        {"You feel Swedish, however that works", Trait.Swedish},
        {"You twitch", Trait.Tourettes},
        {"Your stomach grumbles unpleasantly", Trait.ToxicFarts},
        {"You can't seem to form any coherent thoughts", Trait.Unintelligible},
        {"Your vocal cords feel alien", Trait.GreyVocabulary},
    };

    public static bool IsRandomChanceTrait(Trait trait)
    {
        return trait == Trait.NoBreathing || trait == Trait.Hulk || trait == Trait.XrayVision ||
               trait == Trait.Telekinesis || trait == Trait.Chameleon || trait == Trait.CloakofDarkness;
    }

    public enum TraitType
    {
        Major,
        Intermediate,
        Minor,
        Disabilities
    }

    public static Color GetTraitColor(Gene.Trait trait)
    {
        var traitType = Gene.GetTraitTypeType(trait);

        if (traitType == Gene.TraitType.Major)
        {
            return Color.green;
        }
        else if (traitType == Gene.TraitType.Intermediate)
        {
            return Color.yellow;
        }
        else if (traitType == Gene.TraitType.Minor)
        {
            return Color.cyan;
        }

        return Color.red;
    }

    public static TraitType GetTraitTypeType(Trait trait)
    {
        if ((int) trait <= (int) Trait.Telekinesis)
            return TraitType.Major;

        if ((int) trait <= (int) Trait.SpeedBoost)
            return TraitType.Intermediate;

        if ((int) trait <= (int) Trait.Strong)
            return TraitType.Minor;

        return TraitType.Disabilities;
    }

    public static string GetSymptomForTrait(Trait trait)
    {
        foreach (var sensation in Sensations)
        {
            if (sensation.Value == trait)
            {
                return sensation.Key;
            }
        }

        return "";
    }

    public static int GetTraitTreshold(Gene.Trait trait)
    {
        var traitType = GetTraitTypeType(trait);

        if (traitType == Gene.TraitType.Major)
        {
            return Gene.MajorTreshold;
        }

        if (traitType == Gene.TraitType.Intermediate)
        {
            return Gene.IntermediateTreshold;
        }

        return Gene.MinorTreshold;
    }

    public static HashSet<Gene.Trait> GetSensations(string sensations)
    {
        HashSet<Gene.Trait> activeTraits = new HashSet<Gene.Trait>();

        foreach (var sensation in Sensations)
        {
            if (sensations.Contains(sensation.Key))
            {
                activeTraits.Add(sensation.Value);
            }
        }

        return activeTraits;
    }

    public static int MajorTreshold = int.Parse("DAC", System.Globalization.NumberStyles.HexNumber);
    public static int IntermediateTreshold = int.Parse("BEA", System.Globalization.NumberStyles.HexNumber);
    public static int MinorTreshold = int.Parse("802", System.Globalization.NumberStyles.HexNumber);

    public HashSet<Trait> PossibleTraits = new HashSet<Trait>();
    public HashSet<Trait> ExcludedTraits = new HashSet<Trait>();
    public Trait ConfirmedTrait = Trait.Unknown;
    public bool SuperpowerOrUnknown = false;
    public int value = 0;
    public string hex = "000";
}

public class GeneSystem
{
    public Gene[] Genes = new Gene[55];
    public HashSet<Gene.Trait> AddedTraits = new HashSet<Gene.Trait>();
    public HashSet<Gene.Trait> LastActiveTraits = new HashSet<Gene.Trait>();

    public GeneSystem()
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            Genes[i] = new Gene();
        }
    }

    public void Apply(string genomes, string sensations)
    {
        var activeTraits = Gene.GetSensations(sensations);

        HashSet<Gene.Trait> removedTraits = new HashSet<Gene.Trait>();

        foreach (var trait in LastActiveTraits)
        {
            if (!activeTraits.Contains(trait))
            {
                removedTraits.Add(trait);
            }
        }

        LastActiveTraits = activeTraits;

        int genomeIndex = 0;

        //Updates genes with new genes
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];
            int indexWidth = (i < 9) ? 1 : 2;

            string genomeHex = genomes.Substring(genomeIndex + indexWidth, 3);
            gene.value = int.Parse(genomeHex, System.Globalization.NumberStyles.HexNumber);
            gene.hex = genomeHex;

            genomeIndex += (indexWidth + 3);
        }

        //If gene has a possible trait and the value is below its threshold, and the trait is active somewhere else.
        //Remove it and exclude it
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];

            foreach (var activeTrait in activeTraits)
            {
                if (gene.PossibleTraits.Contains(activeTrait))
                {
                    int threshold = Gene.GetTraitTreshold(activeTrait);
                    
                    if (gene.value < threshold)
                    {
                        gene.PossibleTraits.Remove(activeTrait);
                        gene.ExcludedTraits.Add(activeTrait);
                    }
                }
            }
        }
        
        //Add active traits to possible traits
        //If they are not excluded
        //And they haven't been discovered already
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];

            foreach (var activeTrait in activeTraits)
            {
                if (!AddedTraits.Contains(activeTrait) && !gene.PossibleTraits.Contains(activeTrait) && !gene.ExcludedTraits.Contains(activeTrait))
                {
                    int threshold = Gene.GetTraitTreshold(activeTrait);
                    
                    if (gene.value >= threshold)
                    {
                        gene.PossibleTraits.Add(activeTrait);
                    }
                }
            }
        }
        
        //Exclude traits from other genomes
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];

            foreach (var activeTrait in activeTraits)
            {
                int threshold = Gene.GetTraitTreshold(activeTrait);
                
                if (gene.value >= threshold)
                {
                    for (int j = 0; j < Genes.Length; j++)
                    {
                        var otherGene = Genes[j];
                        if (otherGene.value < threshold)
                        {
                            otherGene.ExcludedTraits.Add(activeTrait);
                        }
                    }
                }
            }
        }

        //Check to see if there is only one possible trait for the gene.
        //Confirm if it is
        //Add trait to AddedTraits

        foreach (var activeTrait in activeTraits)
        {
            int possibleTraitCount = 0;
            int lastIndex = 0;
            for (int i = 0; i < Genes.Length; i++)
            {
                var gene = Genes[i];
                foreach (var possibleTrait in gene.PossibleTraits)
                {
                    if (possibleTrait == activeTrait)
                    {
                        possibleTraitCount++;
                        lastIndex = i;
                    }
                }
            }

            if (possibleTraitCount == 1)
            {
                Genes[lastIndex].ConfirmedTrait = activeTrait;
            }

            if (!Gene.IsRandomChanceTrait(activeTrait))
            {
                AddedTraits.Add(activeTrait);
            }
        }
        
        //Confirm possible trait if no other gene has this possible trait
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];

            if (gene.ConfirmedTrait != Gene.Trait.Unknown)
                continue;
            
            foreach (var possibleTrait in gene.PossibleTraits)
            {
                bool isSingle = true;
                for (int j = 0; j < Genes.Length; j++)
                {
                    if (j == i)
                        continue;
                    
                    var otherGene = Genes[j];
                    if (otherGene.PossibleTraits.Contains(possibleTrait))
                    {
                        isSingle = false;
                    }
                }

                if (isSingle)
                {
                    gene.ConfirmedTrait = possibleTrait;
                    Debug.Log("Confirmed trait because no other gene has this possible trait: " + gene.ConfirmedTrait.ToString());
                }
            }
        }
        
        bool majorSensation = activeTraits.Any(s => Gene.GetTraitTypeType(s) == Gene.TraitType.Major);
        bool intermediateSensation = activeTraits.Any(s => Gene.GetTraitTypeType(s) == Gene.TraitType.Intermediate);
        bool minorSensation = activeTraits.Any(s => Gene.GetTraitTypeType(s) == Gene.TraitType.Minor);
        bool disablitiesSensation = activeTraits.Any(s => Gene.GetTraitTypeType(s) == Gene.TraitType.Disabilities);

        //
        for (int i = 0; i < Genes.Length; i++)
        {
            var gene = Genes[i];

            if (gene.value >= Gene.MajorTreshold && !majorSensation)
            {
                for (int j = 0; j < (int) Gene.Trait.Unknown; j++)
                {
                    var trait = (Gene.Trait) j;

                    if (Gene.GetTraitTypeType(trait) != Gene.TraitType.Major)
                        continue;
                    
                    if (!Gene.IsRandomChanceTrait(trait) && !gene.ExcludedTraits.Contains(trait))
                    {
                        gene.PossibleTraits.Remove(trait);
                        gene.ExcludedTraits.Add(trait);
                        Debug.Log("Excluded major trait: " + trait);
                    }
                }
            }
            
            if (gene.value >= Gene.IntermediateTreshold && !intermediateSensation)
            {
                for (int j = 0; j < (int) Gene.Trait.Unknown; j++)
                {
                    var trait = (Gene.Trait) j;

                    if (Gene.GetTraitTypeType(trait) != Gene.TraitType.Intermediate)
                        continue;
                    
                    if (!Gene.IsRandomChanceTrait(trait) && !gene.ExcludedTraits.Contains(trait))
                    {
                        gene.PossibleTraits.Remove(trait);
                        gene.ExcludedTraits.Add(trait);
                        Debug.Log("Excluded Intermediate trait: " + trait);
                    }
                }
            }
            
            if (gene.value >= Gene.MinorTreshold && !minorSensation)
            {
                for (int j = 0; j < (int) Gene.Trait.Unknown; j++)
                {
                    var trait = (Gene.Trait) j;

                    if (Gene.GetTraitTypeType(trait) != Gene.TraitType.Minor)
                        continue;
                    
                    if (!Gene.IsRandomChanceTrait(trait) && !gene.ExcludedTraits.Contains(trait))
                    {
                        gene.PossibleTraits.Remove(trait);
                        gene.ExcludedTraits.Add(trait);
                        Debug.Log("Excluded trait: " + trait);
                    }
                }
            }
            
            if (gene.value >= Gene.MinorTreshold && !disablitiesSensation)
            {
                for (int j = 0; j < (int) Gene.Trait.Unknown; j++)
                {
                    var trait = (Gene.Trait) j;

                    if (Gene.GetTraitTypeType(trait) != Gene.TraitType.Disabilities)
                        continue;
                    
                    if (!Gene.IsRandomChanceTrait(trait) && !gene.ExcludedTraits.Contains(trait))
                    {
                        gene.PossibleTraits.Remove(trait);
                        gene.ExcludedTraits.Add(trait);
                        Debug.Log("Excluded trait: " + trait);
                    }
                }
            }
        }
    }
}


public class Genetics : MonoBehaviour
{
    private GeneSystem geneSystem;

    void Start()
    {
        geneSystem = new GeneSystem();
    }

    private string genomesString;
    private string sensationsString;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
            && Input.GetKeyDown(KeyCode.V))
        {
            string clipBoard = GUIUtility.systemCopyBuffer;

            string start = "Modify Structural Enzymes";
            string end = "Irradiate Block";

            if (clipBoard.Contains(start) && clipBoard.Contains(end))
            {
                int from = clipBoard.IndexOf(start) + start.Length;
                int to = clipBoard.LastIndexOf(end);

                string genomes = clipBoard.Substring(from, to - from);
                string genomesClean = Regex.Replace(genomes, @"\s+", "");
                genomesString = genomesClean;
            }
            else
            {
                //This a sensations string?
                sensationsString = clipBoard;
            }
        }
    }

    public static void DrawGenomeHex(Rect rect, int index, string hex)
    {
        int genomeValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        if (genomeValue >= Gene.MajorTreshold)
        {
            GUI.color = Color.green;
        }
        else if (genomeValue >= Gene.IntermediateTreshold)
        {
            GUI.color = Color.yellow;
        }
        else if (genomeValue >= Gene.MinorTreshold)
        {
            GUI.color = Color.red;
        }
        else
        {
            GUI.color = Color.white;
        }

        GUI.Label(rect, $"{(index + 1).ToString("00")} : {hex}");
    }

    public static void DrawActiveGenomes(Gene[] genes)
    {
        //Current active genomes
        {
            float x = 50;
            float y = 50;

            float width = 200;
            float height = 15;

            var color = GUI.color;

            GUI.color = Color.white;

            for (int i = 0; i < genes.Length; i++)
            {
                var gene = genes[i];
                string genomeHex = gene.hex;
                DrawGenomeHex(new Rect(x, y + height * i, width, 20), i, genomeHex);
            }

            GUI.color = color;
        }
    }

    public static void DrawGenesToApply(Gene[] genes, string genomes)
    {
        //Genomes to apply
        if (!string.IsNullOrEmpty(genomes))
        {
            float x = 150;
            float y = 50;

            float width = 200;
            float height = 15;

            var color = GUI.color;

            GUI.color = Color.white;

            int genomeIndex = 0;

            for (int i = 0; i < genes.Length; i++)
            {
                int indexWidth = (i < 9) ? 1 : 2;

                string genomeHex = genomes.Substring(genomeIndex + indexWidth, 3);
                DrawGenomeHex(new Rect(x, y + height * i, width, 20), i, genomeHex);

                genomeIndex += (indexWidth + 3);
            }

            GUI.color = color;
        }
    }

    public static void DrawSenations(string sensationsString)
    {
        {
            if (!string.IsNullOrEmpty(sensationsString))
            {
                var color = GUI.color;

                float x = 250;
                float y = 50;
                float width = 200;
                float height = 15;

                int i = 0;

                var sensations = Gene.GetSensations(sensationsString);

                foreach (var sensation in sensations)
                {
                    var trait = sensation;
                    var traitType = Gene.GetTraitTypeType(trait);
                    if (traitType == Gene.TraitType.Major)
                    {
                        GUI.color = Color.green;
                    }
                    else if (traitType == Gene.TraitType.Intermediate)
                    {
                        GUI.color = Color.yellow;
                    }
                    else if (traitType == Gene.TraitType.Minor)
                    {
                        GUI.color = Color.cyan;
                    }
                    else
                    {
                        GUI.color = Color.red;
                    }

                    var rect = new Rect(x, y + height * i, width, 20);
                    GUI.Label(rect, $"{sensation.ToString()}");
                    i++;
                }

                GUI.color = color;
            }
        }
    }

    public static void DrawGeneInfo(Gene[] genes)
    {
        {
            float x = 450;
            float y = 50;

            float width = 1000;
            float textWidth = 35;
            float height = 15;

            var color = GUI.color;

            for (int i = 0; i < genes.Length; i++)
            {
                GUI.color = Color.white;

                var gene = genes[i];

                var confirmButtonRect = new Rect(x - 20, y + height * i, 20, 20);

                if (gene.ConfirmedTrait != Gene.Trait.Unknown)
                {
                    var plusButtonRect = new Rect(x - 40, y + height * i, 20, 20);
                    var minusButtonRect = new Rect(x - 60, y + height * i, 20, 20);
                    if (GUI.Button(plusButtonRect, "+"))
                    {
                        gene.ConfirmedTrait += 1;
                        int length = Enum.GetNames(typeof(Gene.Trait)).Length;

                        if ((int) gene.ConfirmedTrait > length - 2)
                            gene.ConfirmedTrait = (Gene.Trait) length - 2;
                    }

                    if (GUI.Button(minusButtonRect, "-"))
                    {
                        gene.ConfirmedTrait -= 1;

                        if (gene.ConfirmedTrait < 0)
                            gene.ConfirmedTrait = (Gene.Trait) 0;
                    }

                    GUI.color = Gene.GetTraitColor(gene.ConfirmedTrait);
                    var rect = new Rect(x, y + height * i, width, 20);
                    GUI.Label(rect, $"[{gene.ConfirmedTrait.ToString()}]");
                    
                    if (GUI.Button(confirmButtonRect, "C"))
                    {
                        gene.ConfirmedTrait = Gene.Trait.Unknown;
                    }
                    
                    continue;
                }
                else
                {
                    if (GUI.Button(confirmButtonRect, "C"))
                    {
                        //Just set it to something in the middle, use + or - to set it to the one you want
                        gene.ConfirmedTrait = Gene.Trait.Strong;
                    }
                }
                
                var possiblesRect = new Rect(x, y + height * i, 3, 20);

                for (int j = 0; j < (int) Gene.Trait.Unknown; j++)
                {
                    var trait = (Gene.Trait) j;
                    
                    if (!gene.ExcludedTraits.Contains(trait))
                    {
                        GUI.color = Gene.GetTraitColor(trait);
                        GUI.Label(possiblesRect, $"|");
                        possiblesRect.x = possiblesRect.x + 2;
                    }
                }

                for (var j = 0; j < gene.PossibleTraits.Count; j++)
                {
                    var possibleTrait = gene.PossibleTraits.ToList()[j];

                    GUI.color = Gene.GetTraitColor(possibleTrait);

                    string name = possibleTrait.ToString();
                    string namesFull = Regex.Replace(name, "([A-Z])", " $1").Trim();
                    string[] namesSplit = namesFull.Split(' ');

                    string newName = "";

                    try
                    {
                        foreach (var s in namesSplit)
                        {
                            newName += s.Substring(0, 2);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }


                    var rect = new Rect(x + 100 + textWidth * j, y + height * i, width, 20);
                    GUI.Label(rect, $"{newName}");
                }

                GUI.color = color;
            }
        }
    }

    void OnGUI()
    {
        {
            float x = 50;
            float y = 20;

            float width = 200;
            float height = 15;

            if (GUI.Button(new Rect(x, y, width, 20), "Apply"))
            {
                geneSystem.Apply(genomesString, sensationsString);
            }
        }

        var genes = geneSystem.Genes;

        DrawActiveGenomes(genes);
        DrawGenesToApply(genes, genomesString);
        DrawSenations(sensationsString);
        DrawGeneInfo(genes);
    }
}