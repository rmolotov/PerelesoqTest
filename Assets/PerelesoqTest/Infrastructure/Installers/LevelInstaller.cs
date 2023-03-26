using System.Collections.Generic;
using PerelesoqTest.Gameplay.Gadgets;
using UnityEngine;
using Zenject;

namespace PerelesoqTest.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private List<GadgetBaseInfo> gadgets;

        public override void InstallBindings() =>
            gadgets
                .ForEach(g => Container.InjectGameObject(g.gameObject));
    }
}