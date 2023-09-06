namespace GameInstance
{
    public abstract class LevelInstance
    {
        public int uniqIndex {  get; set; }
        public LevelType type { get; set; }
        public LevelState state { get; set; }

        public LevelInstance(int uniqIndex, LevelState state)
        {
            this.uniqIndex = uniqIndex;
            this.state = state;
        }
    }

    public class SimpleLevel : LevelInstance
    {
        //private LevelGenerateSettings levelGenerateSettings;

        public int index { get; set; }
        public bool setting { get; set; }

        public SimpleLevel(int uniqIndex, LevelState state = LevelState.Lock, bool withSetting = false) : base (uniqIndex, state)
        {
            type = LevelType.Simple;
            setting = withSetting;
        }
    }

    public class TutorialLevel : LevelInstance
    {
        public TutorialLevel(int uniqIndex, LevelState state = LevelState.Lock) : base(uniqIndex, state)
        {
            type = LevelType.Tutorial;
        }
    }

    public class UnlimitedLevel : LevelInstance
    {
        public UnlimitedLevel(int uniqIndex, LevelState state = LevelState.Lock) : base(uniqIndex, state) 
        { 
            type = LevelType.Unlimited; 
        }
    }
}
