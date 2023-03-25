using PerelesoqTest.Infrastructure.States;
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
            
        }

        private void BindFactories()
        {
            
        }
    }
}