using System.Collections.Generic;
using System.Threading.Tasks;
using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.StaticData;

namespace PerelesoqTest.Infrastructure.Factories.Interfaces
{
    public interface ILevelFactory
    {
        void Initialize(LevelStaticData levelStaticData);
        Task WarmUp();
        void CleanUp();

        Task<List<GadgetBaseInfo>> CreateLevel();
    }
}