using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityComponents
{
    [CreateAssetMenu(menuName = "LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public int LevelNo = 1;

        public int AsteroidsDestroyed = 0;
        public float TimeSurvived = 0;
        public float DistanceComplete = 0;

        public int AsteroidsGoal = 10;
        public float TimeGoal = 0;
        public float DistanceGoal = 0;

        public int MaxHealth = 3;
        public int CurrentHealth = 3;

        public LevelStates LevelState = LevelStates.Unlocked;

        public enum LevelStates
        {
            Locked, Unlocked, Completed
        }

        public void Restart()
        {
            AsteroidsDestroyed = 0;
            TimeSurvived = 0;
            DistanceComplete = 0;

            CurrentHealth = MaxHealth;
            Static.ui.SetHealth(MaxHealth);
        }

        public void Complete()
        {
            LevelState = LevelStates.Completed;
        }

        public void ReduceHealth()
        {
            CurrentHealth--;
        }
    }
}
