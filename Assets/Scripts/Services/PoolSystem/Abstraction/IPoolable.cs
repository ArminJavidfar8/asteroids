using System;

namespace Services.PoolSystem.Abstaction
{
    public interface IPoolable
    {
        /// <summary>
        /// Name of the prefab.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// This is called only on the first time the GameObject is created
        /// </summary>
        /// <param name="serviceProvider"></param>
        void Initialize(IServiceProvider serviceProvider);
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