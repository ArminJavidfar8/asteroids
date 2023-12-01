using Services.Abstraction;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Abstraction
{
    public interface IAsteroid
    {
        IAsteroidData AsteroidData { get; }
        GameObject TheGameObject { get; }
        Vector3 Position { get; }

        void MoveProperly();
        void Stop();
        void SetPosition(Vector3 position);
    }
}