using BaCon;
using GamePlay.View;
using GameRoot;
using MainMenu;
using R3;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefap;

        public Observable<GameplayExitParams> Run(DIContainer container, GameplayEnterParams enterParams)
        {
            GamePlayRegistrations.Regiter(container, enterParams);
            var gameplayViewModelsContainer = new DIContainer(container);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();

            var uiRoot = container.Resolve<UIViewRoot>();
            var uiScene = Instantiate(_sceneUIRootPrefap);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalsubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalsubj);

            Debug.Log(enterParams.SaveFileName + " " + enterParams.LevelNumber);

            var mainMenuenterParams = new MainMenuEnterParams("fatality");
            var exitParams = new GameplayExitParams(mainMenuenterParams);
            var exitTiMainMenuSceneSignal = exitSceneSignalsubj.Select(_ => exitParams);

            return exitTiMainMenuSceneSignal;
        }
    }
}
