
using System.Threading.Tasks;
using Common;
using DI;
using Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entrypoint
{
    public abstract class GameEntryPointBase
    {
        protected static GameEntryPointBase Instance;
        protected Coroutines Coroutines;
        protected DiContainer RootContainer => rootContainer;

        private readonly DiContainer rootContainer = new();
        private UiRootView uiRootView; // добавлено поле

        public void Initialize()
        {
            Dbg.Log($"Initializing game...", Color.darkOrange);

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            SetupInfrastructure();
            Init();
        }


        protected abstract void RegisterServices();

        protected abstract Task  Init();


        protected virtual void SetupInfrastructure()
        {
            SetupCoroutines();
            // SetupUiRoot();
            RegisterServices();
        }

        private void SetupCoroutines()
        {
            Coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(Coroutines.gameObject);
        }

        private void SetupUiRoot()
        {
            // var prefab = LoadUiRootPrefab();
            // if (prefab == null)
            //     throw new Exception("UiRoot prefab not found!");
            // uiRootView = Object.Instantiate(prefab);
            // Object.DontDestroyOnLoad(uiRootView.gameObject);
            // rootContainer.RegisterInstance(uiRootView);
        }

        // protected virtual void RegisterServices()
        // {
        //rootContainer.RegisterInstance<IUiRoot>(uiRootView);
        //rootContainer.RegisterInstance<IUiManager>(CreateUiManager(uiRootView));
        //rootContainer.RegisterInstance<ISettingsProvider>(CreateSettingsProvider());

        // Добавь другие зависимости по желанию
        // _rootContainer.RegisterInstance<IGameStateProvider>(...);
        //  }
    }
}