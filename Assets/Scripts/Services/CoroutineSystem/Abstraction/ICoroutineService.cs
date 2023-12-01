using System;
using System.Collections;
using UnityEngine;

namespace Services.CoroutineSystem.Abstractio
{
    public interface ICoroutineService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
        void StartDelayedTask(float delay, Action task);
        void DoTaskAtNextFrame(Action checkForWinCondition);
    }
}