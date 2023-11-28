using UnityEngine;

namespace Services.PoolSystem.Abstaction
{
    public interface IPoolService
    {
        /// <summary>
        /// Returns a game object with the given name from pool. Note that the game object must be in PoolData scriptable object.
        /// </summary>
        /// <param name="prefabName">Name of the game object prefab that is in PoolData file</param>
        /// <exception cref="Exception">Throws exeption if there is no prefab with the given name in PoolData file</exception>
        GameObject GetGameObject(string prefabName);
        /// <summary>
        /// Disables the given game object and takes it back to pool.
        /// </summary>
        /// <param name="gameObject">The game object that you want to take it back to pool</param>
        void ReleaseGameObject(GameObject gameObject);
    }
}