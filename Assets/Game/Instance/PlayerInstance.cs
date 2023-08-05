using System.Collections.Generic;

namespace GameInstance
{
    public class PlayerInstance
    {
        private int currentLevel;
        private int choosenLevel;
        private float totalProgress;

        private List<StatisticObject> statistics;
        private Dictionary<int, float> levelResults;

        public float CurrentLevel { get => currentLevel; }
        public float ChoosenLevel { get => choosenLevel; }
        public float TotalProgress { get => totalProgress; set => totalProgress = value; }

        public IEnumerable<StatisticObject> AllStatistics => statistics;
        public IEnumerable<KeyValuePair<int, float>> AllResults => levelResults;

        public PlayerInstance()
        {
            currentLevel = 0;
            choosenLevel = 0;

            totalProgress = 0;

            statistics = new List<StatisticObject>();
            levelResults = new Dictionary<int, float>();
        }

        public PlayerInstance(int curL, int chsL, float total, List<StatisticObject> stat, Dictionary<int, float> res)
        {
            currentLevel = curL;
            choosenLevel = chsL;

            totalProgress = total;

            statistics = stat;
            levelResults = res;
        }

        public void UpdateProgress(float recivedTokens, float allTokens)
        {
            totalProgress = recivedTokens / allTokens * 100;
        }
    }

    public class StatisticObject
    {
        public string Name;
        public int Count;
        public int GoodTouch;
        public int BadTouch;

        public StatisticObject(string name, int count, int goodTouch, int badTouch)
        {
            Name = name;
            Count = count;
            GoodTouch = goodTouch;
            BadTouch = badTouch;
        }
    }
}
