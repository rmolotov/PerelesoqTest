﻿using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    public class Lamp : BaseFunction
    {
        [EnumToggleButtons]
        [SerializeField] private LampChangeType changeType;
        
        [ShowIf("@changeType != LampChangeType.ChangeMaterial")]
        [SerializeField] private List<Light> lights;
        
        [ShowIf("@changeType != LampChangeType.DisableLights")]
        [SerializeField] private MeshRenderer emissiveMesh;
        
        [ShowIf("@changeType != LampChangeType.DisableLights")]
        [SerializeField] private Material onMaterial, offMaterial;
        public override void Activate()
        {
            SwitchLamp(true);
            base.Activate();
        }

        public override void Deactivate()
        {
            SwitchLamp(false);
            base.Deactivate();
        }

        private void SwitchLamp(bool value)
        {
            if (changeType is LampChangeType.DisableLights or LampChangeType.Both)
                lights.ForEach(l => l.enabled = value);

            if (changeType is LampChangeType.ChangeMaterial or LampChangeType.Both)
                emissiveMesh.sharedMaterial = value 
                    ? onMaterial 
                    : offMaterial;
        }
    }
}