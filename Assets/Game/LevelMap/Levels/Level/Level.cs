using LevelSettings;

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

        public SimpleLevel(int myIndex, LevelState state = LevelState.Lock, int uniqIndex = 0) : base (uniqIndex, state)
        {
            type = LevelType.Simple;
            index = myIndex;
        }
    }

    public class TutorialLevel : LevelInstance
    {
        public TutorialObject tutorialObject { get; set; }
        public TutorialLevel(TutorialObject obj, LevelState state = LevelState.Lock, int uniqIndex = 0) : base(uniqIndex, state)
        {
            type = LevelType.Tutorial;
            tutorialObject = obj;
        }
    }

    public class UnlimitedLevel : LevelInstance
    {
        public UnlimitedLevel(LevelState state = LevelState.Lock, int uniqIndex = 0) : base(uniqIndex, state) 
        { 
            type = LevelType.Unlimited; 
        }
    }
}
