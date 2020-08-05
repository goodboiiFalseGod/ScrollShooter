using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class LoseSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        LevelSettings lvl;

        void IEcsRunSystem.Run () {
            // add your run code here.
            if(lvl.CurrentHealth == 0)
            {
                Static.ui.LoseScreen();
                AudioSource.PlayClipAtPoint(config.loseSound, Vector3.zero);
            }
        }
    }
}