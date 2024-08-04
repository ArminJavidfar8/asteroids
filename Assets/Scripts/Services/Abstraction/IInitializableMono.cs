using System;
using UnityEngine;

public abstract class IInitializableMono : MonoBehaviour
{
    public virtual void OnInitialized(IServiceProvider serviceProvider) { }
}
