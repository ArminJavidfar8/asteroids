using UnityEngine;

namespace Management.Abstraction
{
    public interface ISpaceshipController
    {
        Vector3 WeaponPosition { get; }
        Vector3 Up { get; }
        Vector3 Position { get; }

        void OnGetFromPool();
        void OnReleaseToPool();
        void Initialize(int forwardMotorPower, int rotationPower);
        void Move(Vector3 direction);
        void Rotate(Vector2 input);
    }
}