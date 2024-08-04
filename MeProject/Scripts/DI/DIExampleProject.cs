using UnityEngine;

namespace DI
{
    public class MyAwesomeProjectService
    {

    }
    public class MySceneService
    {
        private readonly MyAwesomeProjectService _myAwesomeProject;

        public MySceneService(MyAwesomeProjectService myAwesomeProjectService)
        {
            _myAwesomeProject = myAwesomeProjectService;
        }
    }
    public class MyAwesomeFactory
    {
        public MyAwesomeObject CreateObject(string id, int parameter)
        {
            return new MyAwesomeObject(id, parameter);
        }
    }
    public class MyAwesomeObject
    {
        private readonly string _id;
        private readonly int _parameter;

        public MyAwesomeObject(string id, int parameter)
        {
            _id = id;
            _parameter = parameter;
        }

        public override string ToString()
        {
            return $"{_id} and {_parameter}";
        }
    }

    public class DIExampleProject : MonoBehaviour
    {
        private void Awake()
        {
            var projectContiner = new DIContainer();
            projectContiner.RegisterSingleton(_ => new MyAwesomeProjectService());
            projectContiner.RegisterSingleton("option 1", _ => new MyAwesomeProjectService());
            projectContiner.RegisterSingleton("option 2", _ => new MyAwesomeProjectService());

            var sceneRoot = FindObjectOfType<DIExampleScene>();
            sceneRoot.Init(projectContiner);
        }
    }
}
