using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class AsteroidSpawnSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<Cooldown, GameEntityRef, AsteroidControllerTag> _filterController;
        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterController)
            {
                ref Cooldown cooldownComponent = ref _filterController.Get1(index);
                ref GameEntityRef gameEntityRefComponent = ref _filterController.Get2(index);
                
                if(cooldownComponent.value <= 0)
                {
                    Vector3 spawnPos = new Vector3(Random.Range(0, Screen.width), Screen.height, 0);
                    spawnPos = Camera.main.ScreenToWorldPoint(spawnPos);
                    spawnPos.y += 1f;
                    spawnPos.z = -1;

                    Asteroid asteroid = Pool.asteroids[0];
                    asteroid.MoveFromPool();
                    EcsEntity asteroidEntity = asteroid.entity;

                    ref TransformRef transformRef = ref asteroidEntity.Get<TransformRef>();
                    transformRef.value.position = spawnPos;

                    cooldownComponent.value = config.AsteroidCooldown;
                }
            }
        }
    }
}