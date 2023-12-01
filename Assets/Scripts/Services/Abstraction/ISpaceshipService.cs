using Management.Abstraction;
using UnityEngine;

namespace Services.Abstraction.Spaceship
{
    public interface ISpaceshipService
    {
        ISpaceshipController Player { get; }

        ISpaceshipController CreatePlayer();
        void RemovePlayer();
        ISpaceshipController CreateEnemy(Vector3 position);
        void RemoveEnemy(GameObject enemy);
    }
}