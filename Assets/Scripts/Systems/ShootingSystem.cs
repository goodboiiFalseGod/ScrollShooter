using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

namespace Client {
    sealed class ShootingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<Cooldown, GunsPositions, GunToShoot> _filterShooter;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterShooter)
            {
                ref Cooldown cooldownComponent = ref _filterShooter.Get1(index);
                ref GunsPositions gunsPositionsComponent = ref _filterShooter.Get2(index);
                ref GunToShoot gunToShootComponent = ref _filterShooter.Get3(index);

                if (cooldownComponent.value <= 0)
                {
                    Bullet bullet = Pool.bullets[0];
                    bullet.MoveFromPool();                    
                    EcsEntity bulletEntity = bullet.entity;
                    ref TransformRef transfromRefBullet = ref bulletEntity.Get<TransformRef>();


                    if (!gunToShootComponent.isBoth)
                    {
                        if(gunToShootComponent.left == 1)
                        {
                            transfromRefBullet.value.position = gunsPositionsComponent.left.position;

                            gunToShootComponent.left = 0;
                            gunToShootComponent.right = 1;

                        }
                        else if(gunToShootComponent.right == 1)
                        {
                            transfromRefBullet.value.position = gunsPositionsComponent.right.position;

                            gunToShootComponent.right = 0;
                            gunToShootComponent.left = 1;
                        }
                    }
                    else if(gunToShootComponent.isBoth)
                    {
                        Bullet bullet1 = Pool.bullets[1];
                        bullet1.MoveFromPool();
                        EcsEntity bulletEntity1 = bullet1.entity;
                        ref TransformRef transfromRefBullet1 = ref bulletEntity1.Get<TransformRef>();

                        transfromRefBullet.value.position = gunsPositionsComponent.left.position;

                        transfromRefBullet1.value.position = gunsPositionsComponent.right.position;
                    }

                    AudioSource.PlayClipAtPoint(config.shoot, Vector3.zero);
                    cooldownComponent.value = config.ShootCooldown;
                }
            }
        }
    }
}