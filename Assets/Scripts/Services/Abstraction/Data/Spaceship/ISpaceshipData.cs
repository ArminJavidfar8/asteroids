using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction.Data.Spaceship
{
    public interface ISpaceshipData
    {
        string Id { get; }
        int ForwardMotorPower {  get; }
        int RotationPower {  get; }
    }
}