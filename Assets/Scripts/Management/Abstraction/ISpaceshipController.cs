using UnityEngine;

namespace Management.Abstraction
{
    public interface ISpaceshipController
    {
        void Initialize();
        void MoveForward();
        void Rotate(Vector2 input);
    }
}