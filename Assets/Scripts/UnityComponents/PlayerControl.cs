using Assets.UnityComponents;
using Client;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : GameEntity
{
    Vector3 previousPos;
    Vector3 currentPos;

    public Config config;

    // Update is called once per frame
    void Update()
    {
        float YOffset = 0;
        float XOffset = 0;

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("pressed");
            previousPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;

            YOffset = currentPos.y - previousPos.y;
            XOffset = currentPos.x - previousPos.x;

            if(config.DragDrop)
            {
                previousPos = currentPos;
            }
        }

        Vector3 dir = new Vector3(XOffset, YOffset, 0).normalized;

        float XPower = XOffset * config.Xsens / Screen.width;
        float YPower = YOffset * config.Ysens / Screen.height;

        if (!entity.IsNull())
        {
            if (entity.Has<Direction>())
            {
                ref Direction dirComponent = ref entity.Get<Direction>();
                dirComponent.value = dir;
            }
            if (entity.Has<Power>())
            {
                ref Power powerComponent = ref entity.Get<Power>();
                powerComponent.xPower = Mathf.Abs(XPower);
                powerComponent.yPower = Mathf.Abs(YPower);
            }
        }
    }

    private float GetLargest(float a, float b)
    {
        float ret = a;

        if(b >= a)
        {
            ret = b;
        }

        return ret;
    }   

    protected override void CreateEntity()
    {
        EcsWorld world = Service<EcsWorld>.Get();

        entity = world.NewEntity();

        ref Power powerComponent = ref entity.Get<Power>();
        powerComponent.xPower = 0;
        powerComponent.yPower = 0;

        entity.Get<Direction>().value = Vector3.zero;
    }
}
