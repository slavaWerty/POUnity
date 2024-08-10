using BaCon;
using GamePlay.Service;
using GameRoot.Services;

namespace GamePlay
{
    public static class GamePlayRegistrations
    {
        public static void Regiter(DIContainer container, GameplayEnterParams enterParams)
        {
            container.RegisterFactory(c => new SomeGameplayService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}
