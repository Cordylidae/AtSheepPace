using System;
using System.Collections.Generic;

namespace GameInstance
{
    public class LevelInstance
    {
        private int index;
        private string type;
        private bool status; // open = 1, close = 0
    }

    public class SimpleLevel : LevelInstance
    {
        private int tokens;
        private int number;

        private LevelGenerateSettings levelGenerateSettings;
    }

    public class LevelGenerateSettings
    {
        private int countBaseElements;
        private List<Goal> levelGoals;

        private Tuple<int, int> roundRange;
        private List<string> elementsInclude;
    }

    public struct Goal
    {
        public string name;
        public string count;
    }
}
