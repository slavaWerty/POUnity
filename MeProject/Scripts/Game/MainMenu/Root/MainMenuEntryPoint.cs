using GamePlay;
using GameRoot;
using R3;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefap;

        public Observable<MainMenuExitParams> Run(UIViewRoot uiRoot, MainMenuEnterParams enterParams)
        {
            var uiScene = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubj);

            Debug.Log(enterParams?.Result);

            var savefileName = "ololo.save";
            var levelNumber = Random.Range(0, 300);
            var gameplayEnterParams = new GameplayEnterParams(savefileName, levelNumber);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGameplayScenesSignal = exitSceneSignalSubj.Select(_ => mainMenuExitParams);

            return exitToGameplayScenesSignal;
        }
    }
}
