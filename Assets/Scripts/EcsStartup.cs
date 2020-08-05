using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        public Config config;
        public LevelSettings LevelSettings;

        EcsWorld _world;
        EcsSystems _systems;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            if(Static.CurrentLvl == null)
            {
                Static.CurrentLvl = LevelSettings;
            }

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add(new ControlSystem())
                .Add(new CooldownSystem())
                .Add(new MapMovingSystem())
                .Add(new BulletsPoolCollector())
                .Add(new ShootingSystem())
                .Add(new BulletMovingSystem())
                .Add(new AsteroidSpawnSystem())
                .Add(new AsteroidsMoveSystem())
                .Add(new AsteroidPoolCollector())
                .Add(new CollideSystem())
                .Add(new AsteroidCollidedSystem())
                .Add(new BulletCollideSystem())
                .Add(new ShipCollidedSystem())
                .Add(new LoseSystem())
                .Add(new WinSystem())

                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                // .Inject (new CameraService ())
                // .Inject (new NavMeshSupport ())
                .Inject(config)
                .Inject(Static.CurrentLvl)
                .Init ();

            LeopotamGroup.Globals.Service<EcsWorld>.Set(_world);
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}