using Services.Abstraction;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Abstraction
{
    public interface IAsteroid : IPoolable
    {
        IAsteroidData AsteroidData { get; }
        GameObject TheGameObject { get; }

        void StartMoving();
        void BreakDown();
        void SetPosition(Vector3 position);
    }
}