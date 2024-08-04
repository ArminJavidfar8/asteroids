using Management.Core;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private IInitializableMono[] _initializables;

    protected virtual void Awake()
    {
        IServiceProvider serviceProvider = ServiceHolder.ServiceProvider;

        for (int i = 0; i < _initializables.Length; i++)
        {
            _initializables[i].OnInitialized(serviceProvider);
        }
    }
}
