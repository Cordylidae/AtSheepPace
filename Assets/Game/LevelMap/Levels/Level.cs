using System;
using System.Collections.Generic;

namespace GameInstance
{
    public class LevelInstance
    {
        public int uniqIndex {  get; set; }
        public LevelType type { get; set; }
        public LevelState state { get; set; }

        public LevelInstance(int uniqIndex, LevelType type, LevelState state)
        {
            this.uniqIndex = uniqIndex;
            this.type = type;
            this.state = state;
        }
    }

    public class SimpleLevel : LevelInstance
    {
        //private LevelGenerateSettings levelGenerateSettings;

        public SimpleLevel(int uniqIndex, LevelType type, LevelState state) : base (uniqIndex, type, state)
        {
        }
    }

    public class TutorialLevel : LevelInstance
    {
        public TutorialLevel(int uniqIndex, LevelType type, LevelState state) : base(uniqIndex, type, state)
        {
        }
    }

    public class UnlimitedLevel : LevelInstance
    {
        public UnlimitedLevel(int uniqIndex, LevelType type, LevelState state) : base(uniqIndex, type, state)
        {
        }
    }

    public class LevelGenerateSettings
    {
        private int countBaseElements;
        //private List<Goal> levelGoals;

        private Tuple<int, int> roundRange;
        private List<string> elementsInclude;
    }

    //public struct Goal
    //{
    //    public string name;
    //    public string count;
    //}
}
