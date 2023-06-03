using UnityEngine;

// TODO its like dictionary

[CreateAssetMenu(fileName = "EntitiesForAnimal", menuName = "ScriptableObjects/EntitiesForAnimal", order = 2)]
public class AnimalEntities : ScriptableObject
{
    public GameObject[] animal;
}