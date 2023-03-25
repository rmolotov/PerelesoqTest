using PerelesoqTest.Infrastructure.States;
using PerelesoqTest.Services.Logging;
using Zenject;

namespace PerelesoqTest.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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