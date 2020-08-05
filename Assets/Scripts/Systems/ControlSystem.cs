using Leopotam.Ecs;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client {
    sealed class ControlSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<Direction, Power> _filterControl;
        EcsFilter<Controllable, TransformRef, AllLimits> _filterControllable;

        void IEcsRunSystem.Run () {
            // add your run code here.
            if (config.JoyStick)
            {
                foreach (var index in _filterControl)
                {
                    ref Direction dirComponent = ref _filterControl.Get1(index);
                    ref Power powerComponent = ref _filterControl.Get2(index);

                    foreach (var index1 in _filterControllable)
                    {
                        ref TransformRef transformRefComponent = ref _filterControllable.Get2(index1);
                        ref AllLimits allLimitsComponent = ref _filterControllable.Get3(index1);

                        Vector3 fin = new Vector3(dirComponent.value.x * powerComponent.xPower, dirComponent.value.y * powerComponent.yPower * 1.5f, 0) * config.MaxControlPower / 100f;

                        if (fin.x > 0.1f)
                        {
                            fin.x = 0.1f;
                        }
                        if (fin.x < -0.1f)
                        {
                            fin.x = -0.1f;
                        }

                        if (fin.y > 0.1f)
                        {
                            fin.y = 0.1f;
                        }
                        if (fin.y < -0.1f)
                        {
                            fin.y = -0.1f;
                        }

                        if (fin.x < 0.001f && fin.x > -0.001f)
                        {
                            fin.x = 0;
                        }
                        if (fin.y < 0.001f && fin.y > -0.001f)
                        {
                            fin.y = 0;
                        }

                        Vector3 leftScr = Camera.main.WorldToScreenPoint(allLimitsComponent.left.position);
                        Vector3 rightScr = Camera.main.WorldToScreenPoint(allLimitsComponent.right.position);
                        Vector3 upScr = Camera.main.WorldToScreenPoint(allLimitsComponent.up.position);
                        Vector3 downScr = Camera.main.WorldToScreenPoint(allLimitsComponent.down.position);

                        Vector3 leftDown = new Vector3(0, 0);
                        Vector3 rightUp = new Vector3(Screen.width, Screen.height);

                        if (leftScr.x <= leftDown.x)
                        {
                            if(fin.x < 0)
                            {
                                fin.x = 0;
                            }

                        }
                        if(rightScr.x >= rightUp.x)
                        {
                            if(fin.x > 0)
                            {
                                fin.x = 0;
                            }
                        }
                        if(upScr.y >= rightUp.y)
                        {
                            if(fin.y > 0)
                            {
                                fin.y = 0;
                            }
                        }
                        if(downScr.y <= leftDown.y)
                        {
                            if(fin.y < 0)
                            {
                                fin.y = 0;
                            }
                        }


                        //Debug.Log(leftScr.ToString());

                        transformRefComponent.value.Translate(fin);
                    }
                }
            }
            else if(config.FollowMouse)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    foreach (var index1 in _filterControllable)
                    {
                        ref TransformRef transformRefComponent = ref _filterControllable.Get2(index1);
                        ref AllLimits allLimitsComponent = ref _filterControllable.Get3(index1);

                        if (Input.GetMouseButton(0))
                        {
                            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector3(mousePos.x, mousePos.y, -1);
                            float dist = Vector3.Distance(mousePos, transformRefComponent.value.position);
                            if (dist > 0.1f)
                            {
                                Vector3 dir = (mousePos - transformRefComponent.value.position).normalized;
                                Vector3 fin = dir * config.MaxMouseFollowSpeed * Time.deltaTime;

                                Vector3 leftScr = Camera.main.WorldToScreenPoint(allLimitsComponent.left.position);
                                Vector3 rightScr = Camera.main.WorldToScreenPoint(allLimitsComponent.right.position);
                                Vector3 upScr = Camera.main.WorldToScreenPoint(allLimitsComponent.up.position);
                                Vector3 downScr = Camera.main.WorldToScreenPoint(allLimitsComponent.down.position);

                                Vector3 leftDown = new Vector3(0, 0);
                                Vector3 rightUp = new Vector3(Screen.width, Screen.height);

                                if (leftScr.x <= leftDown.x)
                                {
                                    if (fin.x < 0)
                                    {
                                        fin.x = 0;
                                    }

                                }
                                if (rightScr.x >= rightUp.x)
                                {
                                    if (fin.x > 0)
                                    {
                                        fin.x = 0;
                                    }
                                }
                                if (upScr.y >= rightUp.y)
                                {
                                    if (fin.y > 0)
                                    {
                                        fin.y = 0;
                                    }
                                }
                                if (downScr.y <= leftDown.y)
                                {
                                    if (fin.y < 0)
                                    {
                                        fin.y = 0;
                                    }
                                }

                                transformRefComponent.value.Translate(fin);
                            }
                        }
                    }
                }                
            }

            else if(config.DragDrop)
            {
                foreach (var index in _filterControl)
                {
                    ref Direction dirComponent = ref _filterControl.Get1(index);
                    ref Power powerComponent = ref _filterControl.Get2(index);

                    foreach (var index1 in _filterControllable)
                    {
                        ref TransformRef transformRefComponent = ref _filterControllable.Get2(index1);
                        ref AllLimits allLimitsComponent = ref _filterControllable.Get3(index1);

                        Vector3 fin = new Vector3(dirComponent.value.x * powerComponent.xPower, dirComponent.value.y * powerComponent.yPower * 1.5f, 0) * config.MaxControlPower;

                        if (fin.x > 0.1f)
                        {
                            fin.x = 0.1f;
                        }
                        if (fin.x < -0.1f)
                        {
                            fin.x = -0.1f;
                        }

                        if (fin.y > 0.1f)
                        {
                            fin.y = 0.1f;
                        }
                        if (fin.y < -0.1f)
                        {
                            fin.y = -0.1f;
                        }

                        if (fin.x < 0.001f && fin.x > -0.001f)
                        {
                            fin.x = 0;
                        }
                        if (fin.y < 0.001f && fin.y > -0.001f)
                        {
                            fin.y = 0;
                        }

                        Vector3 leftScr = Camera.main.WorldToScreenPoint(allLimitsComponent.left.position);
                        Vector3 rightScr = Camera.main.WorldToScreenPoint(allLimitsComponent.right.position);
                        Vector3 upScr = Camera.main.WorldToScreenPoint(allLimitsComponent.up.position);
                        Vector3 downScr = Camera.main.WorldToScreenPoint(allLimitsComponent.down.position);

                        Vector3 leftDown = new Vector3(0, 0);
                        Vector3 rightUp = new Vector3(Screen.width, Screen.height);

                        if (leftScr.x <= leftDown.x)
                        {
                            if (fin.x < 0)
                            {
                                fin.x = 0;
                            }

                        }
                        if (rightScr.x >= rightUp.x)
                        {
                            if (fin.x > 0)
                            {
                                fin.x = 0;
                            }
                        }
                        if (upScr.y >= rightUp.y)
                        {
                            if (fin.y > 0)
                            {
                                fin.y = 0;
                            }
                        }
                        if (downScr.y <= leftDown.y)
                        {
                            if (fin.y < 0)
                            {
                                fin.y = 0;
                            }
                        }

                        transformRefComponent.value.Translate(fin);
                    }
                }
            }
        }
    }
}