using Leopotam.Ecs;

namespace Client {
    sealed class BulletCollideSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<Collided, BulletTag, GameEntityRef> _filterBullet;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterBullet)
            {
                ref GameEntityRef gameEntityRefComponent = ref _filterBullet.Get3(index);

                Bullet bullet = (Bullet)gameEntityRefComponent.value;

                bullet.MoveToPool();

                _filterBullet.GetEntity(index).Del<Collided>();
            }
        }
    }
}