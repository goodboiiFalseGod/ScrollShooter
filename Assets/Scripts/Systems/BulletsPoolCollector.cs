using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class BulletsPoolCollector : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<BulletTag, TransformRef, GameEntityRef>.Exclude<InPool> _filterBullets;

        void IEcsRunSystem.Run () {
            // add your run code here.

            foreach (var index1 in _filterBullets)
            {
                ref TransformRef bulletTransformRefComponent = ref _filterBullets.Get2(index1);
                ref GameEntityRef bulletGameEntityRefComponent = ref _filterBullets.Get3(index1);

                float dist = Vector3.Distance(Vector3.zero, bulletTransformRefComponent.value.position);
                if (dist > 36)
                {
                    Bullet bullet = (Bullet)bulletGameEntityRefComponent.value;
                    bullet.MoveToPool();
                }
            }

        }
    }
}