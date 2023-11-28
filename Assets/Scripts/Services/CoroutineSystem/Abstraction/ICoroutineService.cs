using System.Collections;
using UnityEngine;

namespace Services.CoroutineSystem.Abstractio
{
    public interface ICoroutineService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}