using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class AsteroidsMoveSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<EnemyTag, Direction, TransformRef>.Exclude<InPool> _filterMovingEnemy;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterMovingEnemy)
            {
                ref TransformRef transformRefComponent = ref _filterMovingEnemy.Get3(index);
                ref Direction directionComponent = ref _filterMovingEnemy.Get2(index);

                Vector3 move = directionComponent.value * Time.deltaTime * config.GameSpeed * 10f;
                move.z = 0;

                transformRefComponent.value.Translate(move, Space.World);
            }
        }
    }
}