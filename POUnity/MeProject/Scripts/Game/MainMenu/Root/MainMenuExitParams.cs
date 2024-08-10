using GameRoot;

namespace MainMenu
{
    public class MainMenuExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}
