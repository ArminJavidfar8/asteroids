using Services.CoroutineSystem.Abstractio;
using Services.EventSystem.Abstraction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Services.CoroutineSystem.Core
{
    public class CoroutineService : ICoroutineService
    {
        private CoroutinesHolder _coroutinesHolder;
        
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (_coroutinesHolder == null)
            {
                _coroutinesHolder = new GameObject("CoroutinesHolder").AddComponent<CoroutinesHolder>();
            }
            return _coroutinesHolder.StartCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            _coroutinesHolder.StopCoroutine(routine);
        }

        public void StartDelayedTask(float delay, Action task)
        {
            StartCoroutine(DoDelayedTask(delay, task));
        }

        private IEnumerator DoDelayedTask(float delay, Action task)
        {
            yield return new WaitForSeconds(delay);
            task?.Invoke();
        }
    }
}