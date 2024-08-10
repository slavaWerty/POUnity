using GamePlay;
using MainMenu;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using R3;
using BaCon;
using GameRoot.Services;

namespace GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIViewRoot _uiRoot;
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefapUIRoot = Resources.Load<UIViewRoot>("UIRoot");
            _uiRoot = Object.Instantiate(prefapUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);

            _rootContainer.RegisterFactory(C => new SomeCommonService()).AsSingle();
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                var enterParams = new GameplayEnterParams("ddd.save", 1);
                _coroutines.StartCoroutine(LoadAndStartGamePlay(enterParams));
                return;
            }

            if (sceneName == Scenes.MAINMENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartGamePlay(GameplayEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1f);

            var sceneEntryPoint = Object.FindObjectOfType<GamePlayEntryPoint>();
            var gamePlayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(gamePlayContainer, enterParams).Subscribe(gameplayEnterParams => 
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayEnterParams.MainMenuEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAINMENU);

            yield return new WaitForSeconds(1f);

            var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
            var mainMenuContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if(targetSceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndStartGamePlay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
