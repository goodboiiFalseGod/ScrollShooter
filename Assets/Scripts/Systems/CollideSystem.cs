using Leopotam.Ecs;
using System.Reflection;
using UnityEngine;

namespace Client {
    sealed class CollideSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<PlayerTag, TransformRef, ColliderRadius> _playerFilter;
        EcsFilter<BulletTag, TransformRef, ColliderRadius>.Exclude<InPool> _bulletFilter;
        EcsFilter<EnemyTag, TransformRef, ColliderRadius>.Exclude<InPool, Exploded> _asteroidFilter;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _asteroidFilter)
            {
                ref TransformRef asteroidTransformRefComponent = ref _asteroidFilter.Get2(index);
                ref ColliderRadius asteroidColliderRadiusComponent = ref _asteroidFilter.Get3(index);

                foreach(var index1 in _bulletFilter)
                {
                    ref TransformRef bulletTransfromRefComponent = ref _bulletFilter.Get2(index1);
                    ref ColliderRadius bulletColliderRadiusComponent = ref _bulletFilter.Get3(index1);
                    float dis = Vector3.Distance(asteroidTransformRefComponent.value.position, bulletTransfromRefComponent.value.position);

                    if(dis < asteroidColliderRadiusComponent.value + bulletColliderRadiusComponent.value)
                    {
                        _asteroidFilter.GetEntity(index).Get<Collided>();
                        _bulletFilter.GetEntity(index1).Get<Collided>();
                    }
                }

                foreach(var index1 in _playerFilter)
                {
                    ref TransformRef playerTransformRefComponent = ref _playerFilter.Get2(index1);
                    ref ColliderRadius playerColliderRadiusComponent = ref _asteroidFilter.Get3(index);
                    float dis = Vector3.Distance(asteroidTransformRefComponent.value.position, playerTransformRefComponent.value.position);

                    if (dis <= playerColliderRadiusComponent.value + asteroidColliderRadiusComponent.value)
                    {
                        _asteroidFilter.GetEntity(index).Get<Collided>();
                        _playerFilter.GetEntity(index1).Get<Collided>();
                    }
                }
            }
        }
    }
}