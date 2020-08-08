using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity
{
    public Config config;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform leftGun;
    public Transform rightGun;

    protected override void CreateEntity()
    {
        EcsWorld ecsWorld = Service<EcsWorld>.Get();

        entity = ecsWorld.NewEntity();

        ref AllLimits allLimitsComponent = ref entity.Get<AllLimits>();
        allLimitsComponent.up = up;
        allLimitsComponent.down = down;
        allLimitsComponent.left = left;
        allLimitsComponent.right = right;

        ref GunsPositions gunsPositionsComponent = ref entity.Get<GunsPositions>();
        gunsPositionsComponent.left = leftGun;
        gunsPositionsComponent.right = rightGun;

        ref GunToShoot gunToShootComponent = ref entity.Get<GunToShoot>();
        gunToShootComponent.left = 0;
        gunToShootComponent.right = 1;
        gunToShootComponent.isBoth = config.IsBothGunsShooting;

        entity.Get<Cooldown>().value = 0;
        entity.Get<GameEntityRef>().value = this;
        entity.Get<TransformRef>().value = this.transform;
        entity.Get<ColliderRadius>().value = this.GetComponent<CircleCollider2D>().radius;

        entity.Get<Controllable>();
        entity.Get<PlayerTag>();
    }
}
