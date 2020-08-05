using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class AsteroidCollidedSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        LevelSettings lvl;
        Config config;

        EcsFilter<Collided, EnemyTag, GameEntityRef> _filterAsteroid;

        void IEcsRunSystem.Run () {
            // add your run code here.
            foreach(var index in _filterAsteroid)
            {
                ref GameEntityRef gameEntityRefComponent = ref _filterAsteroid.Get3(index);
                Asteroid asteroid = (Asteroid)gameEntityRefComponent.value;

                asteroid.Boom();

                _filterAsteroid.GetEntity(index).Get<Exploded>();
                _filterAsteroid.GetEntity(index).Del<Collided>();

                lvl.AsteroidsDestroyed++;

                Static.ui.goal.text = lvl.AsteroidsDestroyed.ToString() + "/" + lvl.AsteroidsGoal.ToString();

                AudioSource.PlayClipAtPoint(config.explosion, Vector3.zero);
            }
        }
    }
}