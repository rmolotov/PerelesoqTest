using PerelesoqTest.Infrastructure.States;
using Zenject;

namespace PerelesoqTest.Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadMetaState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
    }
}