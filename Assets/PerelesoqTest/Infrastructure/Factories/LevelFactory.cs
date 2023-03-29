using System.Collections.Generic;
using System.Threading.Tasks;
using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.StaticData;

namespace PerelesoqTest.Infrastructure.Factories
{
    public class LevelFactory: ILevelFactory
    {
        private LevelStaticData _levelStaticData;

        public void Initialize(LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;
        }

        public async Task WarmUp()
        {
            
        }

        public void CleanUp()
        {
            
        }

        public Task<List<GadgetBaseInfo>> CreateLevel() => 
            Task.FromResult(_levelStaticData.Gadgets);
    }
}