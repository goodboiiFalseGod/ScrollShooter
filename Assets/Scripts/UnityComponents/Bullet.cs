using Assets.Scripts.UnityComponents;
using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameEntity
{
    protected override void CreateEntity()
    {
        EcsWorld world = Service<EcsWorld>.Get();

        entity = world.NewEntity();

        entity.Get<OnScreen>().time = 0;
        entity.Get<TransformRef>().value = this.transform;
        entity.Get<GameEntityRef>().value = this;
        entity.Get<BulletTag>();

        entity.Get<ColliderRadius>().value = this.GetComponent<CircleCollider2D>().radius;
        

        MoveToPool();
    }

    public override void MoveToPool()
    {
        base.MoveToPool();
        Pool.bullets.Add(this);

        //Debug.Log(Pool.bullets.Count.ToString());
    }

    public override void MoveFromPool()
    {
        base.MoveFromPool();
        Pool.bullets.Remove(this);
    }
}
