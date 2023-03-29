using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.StaticData;

namespace PerelesoqTest.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private List<GadgetBaseInfo> gadgets;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelInstaller>().FromInstance(this).AsSingle();
        }

        public void Initialize()
        {
            // TODO: using configs via StaticDataService and spawnable gadgets is better but
            // level data constructed from Level scene-layer at this moment
            // this installer and LevelFactory should be reworked
            
            var levelStaticData = new LevelStaticData
            {
                Gadgets = gadgets
            };

            var levelFactory = Container.Resolve<ILevelFactory>();
            levelFactory.Initialize(levelStaticData);

            levelStaticData.Gadgets
                .ForEach(g => Container.InjectGameObject(g.gameObject));
        }
    }
}