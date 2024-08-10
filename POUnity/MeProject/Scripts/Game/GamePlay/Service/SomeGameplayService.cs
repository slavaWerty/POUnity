using GameRoot.Services;
using System;
using UnityEngine;

namespace GamePlay.Service
{
    public class SomeGameplayService : IDisposable
    {
        private readonly SomeCommonService _someGameplayService;

        public SomeGameplayService(SomeCommonService someGameplayService)
        {
            _someGameplayService = someGameplayService;
            Debug.Log(GetType().Name + "has been created");
        }

        public void Dispose()
        {
            Debug.Log("Почистили подписьки");
        }
    }
}
