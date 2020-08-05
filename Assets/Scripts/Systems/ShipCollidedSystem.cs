using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class ShipCollidedSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;
        LevelSettings lvl;
        EcsFilter<PlayerTag, Collided> player;

        void IEcsRunSystem.Run () {
            // add your run code here.
            {
                foreach(var index in player)
                {
                    lvl.ReduceHealth();
                    Static.ui.SetHealth(lvl.CurrentHealth);
                    player.GetEntity(index).Del<Collided>();
                    AudioSource.PlayClipAtPoint(config.reciveDmg, Vector3.zero);
                }
            }
        }
    }
}