using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : GameEntity
{
    // Start is called before the first frame update
    protected override void CreateEntity()
    {
        EcsWorld world = Service<EcsWorld>.Get();

        entity = world.NewEntity();

        entity.Get<Cooldown>().value = 0;
        entity.Get<GameEntityRef>().value = this;
        entity.Get<AsteroidControllerTag>();
    }
}
