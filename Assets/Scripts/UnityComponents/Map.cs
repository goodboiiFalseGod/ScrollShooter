using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : GameEntity
{
    public Transform up;
    public Transform down;

    private void OnBecameVisible()
    {   
        if(entity.Has<NotOnScreen>())
        {
            entity.Del<NotOnScreen>();
        }

        Debug.Log("onscreen");
        entity.Get<OnScreen>();
    }

    private void OnBecameInvisible()
    {
        if (entity.Has<OnScreen>())
        {
            entity.Del<OnScreen>();
        }

        Debug.Log("notonscreen");
        entity.Get<NotOnScreen>();
    }

    protected override void CreateEntity()
    {
        EcsWorld world = Service<EcsWorld>.Get();

        entity = world.NewEntity();

        ref TransformRef transformRefComponent = ref entity.Get<TransformRef>();
        transformRefComponent.value = this.transform;

        ref GameEntityRef gameEntityComponent = ref entity.Get<GameEntityRef>();
        gameEntityComponent.value = this;

        entity.Get<MapTag>();
    }
}
