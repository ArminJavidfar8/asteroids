using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.PoolSystem.Abstaction
{
    public interface IPoolable
    {
        /// <summary>
        /// Name of the prefab.
        /// </summary>
        string Name { get; }
        void Initialize();
        /// <summary>
        /// This is called everytime a game object is got from pool.
        /// </summary>
        void OnGetFromPool();
        /// <summary>
        /// This is called Everytime a game object is going back to pool.
        /// </summary>
        void OnReleaseToPool();
    }
}