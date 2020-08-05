using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class WinSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;

        Config config;
        LevelSettings lvl; 

        void IEcsRunSystem.Run () {
            // add your run code here.
            Debug.Log(lvl.AsteroidsDestroyed.ToString() + "     " + lvl.DistanceComplete.ToString() + "     " + lvl.TimeSurvived.ToString());

            if (lvl.AsteroidsDestroyed >= lvl.AsteroidsGoal && lvl.DistanceComplete >= lvl.DistanceGoal && lvl.TimeSurvived >= lvl.TimeGoal)
            {
                Static.ui.WinScreen();
                lvl.Complete();
                AudioSource.PlayClipAtPoint(config.win, Vector3.zero);
            }
        }
    }
}