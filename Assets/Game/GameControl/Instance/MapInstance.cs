using System.Collections.Generic;

namespace GameInstance
{
    public class MapInstance
    {
        private List<LevelInstance> levels;
        private int tokensReceived;

        private List<Gate> gates;
    }

    public class Gate
    {
        private int index;
        private bool status; // open = 1, close = 0
        private int needForOpen;
    }
}
