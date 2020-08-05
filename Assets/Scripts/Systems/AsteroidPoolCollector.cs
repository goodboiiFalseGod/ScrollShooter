using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class AsteroidPoolCollector : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<EnemyTag, GameEntityRef, TransformRef>.Exclude<InPool> _filterMovingEnemy;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterMovingEnemy)
            {
                ref TransformRef TransformRefComponent = ref _filterMovingEnemy.Get3(index);
                ref GameEntityRef GameEntityRefComponent = ref _filterMovingEnemy.Get2(index);

                float dist = Vector3.Distance(new Vector3(0, 0, -1), TransformRefComponent.value.position);
                if (dist > 36)
                {
                    //Debug.Log(TransformRefComponent.value.position);
                    Asteroid asteroid = (Asteroid)GameEntityRefComponent.value;
                    asteroid.MoveToPool();
                }
            }
        }
    }
}