using Services.CoroutineSystem.Abstractio;
using System;
using System.Collections;
using UnityEngine;

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

        public Coroutine StartDelayedTask(float delay, Action task)
        {
            return StartCoroutine(StartDelayedTaskRoutine(delay, task));
        }

        private IEnumerator StartDelayedTaskRoutine(float delay, Action task)
        {
            yield return new WaitForSeconds(delay);
            task?.Invoke();
        }

        public void DoTaskAtNextFrame(Action task)
        {
            StartCoroutine(DoTaskAtNextFrameRoutine(task));
            
        }

        private IEnumerator DoTaskAtNextFrameRoutine(Action task)
        {
            yield return null;
            task?.Invoke();
        }
    }
}