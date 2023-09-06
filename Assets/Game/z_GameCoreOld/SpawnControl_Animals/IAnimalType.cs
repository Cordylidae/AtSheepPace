using Inspector;
using UnityEngine;
using System.Collections.Generic;

public struct AnimalType
{
    public const string Sheep = "Sheep";
    public const string SheepExtra = "SheepExtra";
    public const string SheepGolden = "SheepGolden";

    public const string Wolf = "Wolf";
    public const string WolfInSheep = "WolfInSheep";
    public const string WolfAlfa = "WolfAlfa";

    public const string Deer = "Deer";
    public const string Boar = "Boar";
    public const string Hedgehog = "Hedgehog";

    public const string Crow = "Crow";
    public const string Fox = "Fox";
    public const string Bear = "Bear";

    public const string Dragon = "Dragon";
    public const string RamGolden = "RamGolden";

    public const string Hare = "Hare";
    public const string Squirrel = "Squirrel";
    public const string Beaver = "Beaver";
    public const string Badger = "Badger";
};

public class IAnimalType : MonoBehaviour
{
    [SerializeField, Inspector.ValueList("AllowedTypes")]
    public string buttonType;
    public virtual List<string> AllowedTypes()
    {
        return typeof(AnimalType).GetAllPublicConstantValues<string>();
    }
}

