using Inspector;
using UnityEngine;
using System.Collections.Generic;

public struct AnimalType
{
    public const string Sheep = "Sheep";
    public const string Wolf = "Wolf";

    public const string Deer = "Deer";
    public const string Boar = "Boar";
    public const string Hedgehog = "Hedgehog";
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

