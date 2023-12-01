using UnityEngine;

namespace Management.Abstraction
{
    public interface ISpaceshipController
    {
        Vector3 BulletPosition { get; }
        Vector3 Up { get; }
        void Initialize();
        void MoveForward();
        void Rotate(Vector2 input);
    }
}