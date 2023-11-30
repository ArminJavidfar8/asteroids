using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface ILevelService
    {
        int CurrentLevel {  get; }
        int TotalAsteroids {  get; }
        int LevelSize {  get; }
        float AsteroidInstantiationRatio {  get; }
    }
}