using UnityEngine;

namespace DI
{
    public class DIExampleScene : MonoBehaviour
    {
        public void Init(DIContainer projectContainer)
        {
            var serviceWithoutTag = projectContainer.Resolve<MyAwesomeProjectService>();
            var service1 = projectContainer.Resolve<MyAwesomeProjectService>("option 1");
            var service2 = projectContainer.Resolve<MyAwesomeProjectService>("option 2");

            var sceneContainer = new DIContainer(projectContainer);
            sceneContainer.RegisterSingleton(c => new MySceneService(c.Resolve<MyAwesomeProjectService>()));
            sceneContainer.RegisterSingleton(_ => new MyAwesomeFactory());
            sceneContainer.RegisterInstance(new MyAwesomeObject("instance", 10));

            var objectFactory = sceneContainer.Resolve<MyAwesomeFactory>();

            for (int i = 0; i < 3; i++)
            {
                var id = $"o{i}";
                var o = objectFactory.CreateObject(id, i);
                Debug.Log(o);
            }

            var instance = sceneContainer.Resolve<MyAwesomeObject>();
            Debug.Log(instance);
        }

    }
}
