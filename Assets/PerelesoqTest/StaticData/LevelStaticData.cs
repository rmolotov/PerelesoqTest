using System;
using System.Collections.Generic;
using PerelesoqTest.Gameplay.Gadgets;

namespace PerelesoqTest.StaticData
{
    [Serializable]
    public record LevelStaticData()
    {
        public List<GadgetBaseInfo> Gadgets { get; set; }
    }
}