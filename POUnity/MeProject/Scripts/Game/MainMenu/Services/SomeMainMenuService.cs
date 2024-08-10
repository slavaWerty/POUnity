using GameRoot.Services;
using UnityEngine;

namespace MainMenu.Services
{
    public class SomeMainMenuService
    {
        private readonly SomeCommonService _someCommonService;

        public SomeMainMenuService(SomeCommonService someCommonService)
        {
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + "has been created"); 
        }
    }
}
