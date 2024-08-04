using R3;
using UnityEngine;

namespace MainMenu
{
    public class UIMainMenuRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignalSubj;

        public void HandleGoToGameplayButtonClick()
        {
            _exitSceneSignalSubj?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exittSceneSignalSubj)
        {
            _exitSceneSignalSubj = exittSceneSignalSubj;
        }
    }
}
