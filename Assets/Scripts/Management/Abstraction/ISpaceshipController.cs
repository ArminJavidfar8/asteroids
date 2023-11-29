using Services.Abstraction.Data.Spaceship;
using System.Collections;
using System.Collections.Generic;
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