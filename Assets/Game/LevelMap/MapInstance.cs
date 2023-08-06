using System.Collections.Generic;

namespace GameInstance
{
    public class MapInstance
    {
        public List<LevelInstance> levels;
        public int currentUniqIndex;

        public MapInstance(List<LevelInstance> _levels)
        {
            levels = _levels;
            currentUniqIndex = 0;
        }

        public void CompleteLevel() 
        {
            if (levels[currentUniqIndex].state == LevelState.New)
            {
                if(currentUniqIndex + 1 < levels.Count) levels[currentUniqIndex + 1].state = LevelState.New;
                levels[currentUniqIndex].state = LevelState.Open;
            }
        }
    }
}
