using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelSettings {

    public class LevelSettingsView : MonoBehaviour
    {
        [SerializeField] private LevelSettingsModel levelSettingsModel;
        public LevelSettingsModel LevelSettings => levelSettingsModel;

        private void Awake()
        {
            SetUniqEffects();
            SetUniqAnimals();
        }

        [Button] private void SetUniqEffects()
        {
            levelSettingsModel.SetUniqEffect();
        }

        [Button] private void SetUniqAnimals()
        {
            levelSettingsModel.SetUniqAnimals();
        }
    }

    [System.Serializable]
    public class LevelSettingsModel
    {
        public FearParameters fearParam;
        public List<LevelEffects> effects;
        public List<LevelAnimal> animals;

        public void SetUniqEffect()
        {
            effects = effects.Distinct().ToList();
        }

        public void SetUniqAnimals()
        {
            animals = animals.Distinct().ToList();
        }

    }

    [System.Serializable]
    public class FearParameters
    {
        public float sizePoints;
    }

    public enum LevelEffects 
    {
        FEAR_BAR, DAY_NIGHT, WOLF_TIME, WIND, RAIN, FOG, THORN, FIRE_EMBER
    }

    public enum LevelAnimal
    {
     Sheep ,
     SheepExtra ,
     SheepGolden ,

     Wolf ,
     WolfInSheep ,
     WolfAlfa ,

     Deer ,
     Boar ,
     Hedgehog ,

     Crow ,
     Fox,
     Bear ,

     Dragon ,
     RamGolden ,

     Hare ,
     Squirrel ,
     Beaver ,
     Badger
    };
}
