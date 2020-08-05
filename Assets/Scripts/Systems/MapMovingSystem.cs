using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class MapMovingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;

        EcsFilter<TransformRef, MapTag> _filterMap;
        EcsFilter<TransformRef, PlayerTag, AllLimits, GunToShoot> _filterPlayer;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index1 in _filterPlayer)
            {
                ref TransformRef playerTransformRefComponent = ref _filterPlayer.Get1(index1);

                foreach (var index in _filterMap)
                {
                    //Debug.Log("s");
                    ref TransformRef transformRefComponent = ref _filterMap.Get1(index);

                    Vector3 down = new Vector3(0, -1, 0);
                    transformRefComponent.value.Translate(down * Time.deltaTime * config.GameSpeed * 10f);
                    
                    if(transformRefComponent.value.position.y <= -17.5)
                    {
                        transformRefComponent.value.Translate(new Vector3(0, 35, 0));
                    }
                }
            }
        }
    }
}