using Assets.Scripts.UnityComponents;
using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameEntity
{
    public GameObject explosion;

    protected override void Start()
    {
        base.Start();
        explosion.SetActive(false);
    }

    protected override void CreateEntity()
    {
        EcsWorld world = Service<EcsWorld>.Get();
        entity = world.NewEntity();

        ref TransformRef transfromRefComponent = ref entity.Get<TransformRef>();
        transfromRefComponent.value = this.transform;

        ref GameEntityRef gameEntityRefComponent = ref entity.Get<GameEntityRef>();
        gameEntityRefComponent.value = this;

        ref Direction enemyDirectionComponent = ref entity.Get<Direction>();
        enemyDirectionComponent.value = new Vector3(Random.Range(-2f, 2f) / 10f, -1, -1);
        Vector3 dir = new Vector3(enemyDirectionComponent.value.x, enemyDirectionComponent.value.y, 0);
        //transform.up = -dir;

        Vector3 angle = transform.up - enemyDirectionComponent.value;
        //transform.rotation = new Quaternion(angle.x * 10f, angle.y * 10f, 0, 0);

        entity.Get<EnemyTag>();

        MoveToPool();
    }

    public override void MoveToPool()
    {
        base.MoveToPool();
        Pool.asteroids.Add(this);

        //Debug.Log(Pool.asteroids.Count.ToString());
    }

    public override void MoveFromPool()
    {
        base.MoveFromPool();
        Pool.asteroids.Remove(this);
    }

    public void Boom()
    {
        explosion.SetActive(true);

        StartCoroutine(CallDelayedAction(() => explosion.SetActive(false), 1f));
        StartCoroutine(CallDelayedAction(MoveToPool, 1f));
        entity.Del<Exploded>();
    }
}
