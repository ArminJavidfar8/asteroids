using Services.CoroutineSystem.Abstractio;
using Services.EventSystem.Abstraction;
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
    }
}