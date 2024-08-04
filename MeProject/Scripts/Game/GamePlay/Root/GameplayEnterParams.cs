using GameRoot;
using Utils;

namespace GamePlay
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }

        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}
