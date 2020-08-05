using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class CooldownSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        EcsFilter<Cooldown> ecsFilter;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in ecsFilter)
            {
                ref Cooldown cooldownComponent = ref ecsFilter.Get1(index);

                cooldownComponent.value -= 1 * Time.deltaTime;
            }
        }
    }
}