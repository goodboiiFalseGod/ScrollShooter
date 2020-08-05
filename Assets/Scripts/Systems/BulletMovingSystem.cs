using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class BulletMovingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<BulletTag, TransformRef>.Exclude<InPool> _filterBullet;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterBullet)
            {
                ref TransformRef transformRefComponent = ref _filterBullet.Get2(index);

                Vector3 dir = new Vector3(0, 1, 0);
                Vector3 move = dir * config.BulletSpeed * Time.deltaTime * 10f;

                transformRefComponent.value.Translate(move);
            }
        }
    }
}