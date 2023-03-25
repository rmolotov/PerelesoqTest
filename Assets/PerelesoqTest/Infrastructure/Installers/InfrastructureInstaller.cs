using PerelesoqTest.Infrastructure.AssetManagement;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States;
using PerelesoqTest.Services.Logging;
using Zenject;

namespace PerelesoqTest.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AddressableProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            
            BindServices();
            BindFactories();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle(); //GameStateMachine entry point is Initialize()
        }

        private void BindServices()
        {
            Container.Bind<ILoggingService>().To<LoggingService>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            
        }
    }
}