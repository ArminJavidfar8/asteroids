using Services.Data.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface ILevelService
    {
        int CurrentLevelNumber { get; }
        ILevelData CurrentLevelData {  get; }

        ILevelData[] ReadLevelsData();
        int CalculateTotalAstroids(int largeNumbers, int mediumNumbers, int smallNumbers);
        int CurrentLevelTotalAstroids();
    }
}