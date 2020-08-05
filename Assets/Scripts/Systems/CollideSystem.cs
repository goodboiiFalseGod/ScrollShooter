using Leopotam.Ecs;
using System.Reflection;
using UnityEngine;

namespace Client {
    sealed class CollideSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<PlayerTag, TransformRef> _playerFilter;
        EcsFilter<BulletTag, TransformRef>.Exclude<InPool> _bulletFilter;
        EcsFilter<EnemyTag, TransformRef>.Exclude<InPool> _asteroidFilter;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _asteroidFilter)
            {
                ref TransformRef asteroidTransformRefComponent = ref _asteroidFilter.Get2(index);

                foreach(var index1 in _bulletFilter)
                {
                    ref TransformRef bulletTransfromRefComponent = ref _bulletFilter.Get2(index1);

                    float dis = Vector3.Distance(asteroidTransformRefComponent.value.position, bulletTransfromRefComponent.value.position);

                    if(dis < 0.95f)
                    {
                        _asteroidFilter.GetEntity(index).Get<Collided>();
                        _bulletFilter.GetEntity(index1).Get<Collided>();
                    }
                }

                foreach(var index1 in _playerFilter)
                {
                    ref TransformRef playerTransformRefComponent = ref _playerFilter.Get2(index1);

                    float dis = Vector3.Distance(asteroidTransformRefComponent.value.position, playerTransformRefComponent.value.position);

                    if(dis < 2f)
                    {
                        _asteroidFilter.GetEntity(index).Get<Collided>();
                        _playerFilter.GetEntity(index1).Get<Collided>();
                    }
                }
            }
        }
    }
}