using Client;
using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityComponents
{
    public abstract class GameEntity : MonoBehaviour
    {
        public EcsEntity entity;

        protected virtual void Start()
        {
            StartCoroutine(CallDelayedAction(CreateEntity, 1));
        }

        protected static IEnumerator CallDelayedAction (Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        protected static IEnumerator CallDelayedAction(Action action, int frames)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }

            action.Invoke();
        }

        protected abstract void CreateEntity();

        protected virtual void OnDestroy()
        {
            entity.Destroy();
        }

        public virtual void MoveToPool()
        {
            this.gameObject.SetActive(false);
            entity.Get<InPool>();
        }

        public virtual void MoveFromPool()
        {
            this.gameObject.SetActive(true);
            entity.Del<InPool>();
        }
    }
}
