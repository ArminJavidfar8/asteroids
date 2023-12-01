using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using Services.Data.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class LevelServiceTest
    {
        [Test]
        public void TestReadingLevelsData()
        {
            ILevelService levelService = ServiceHolder.ServiceProvider.GetService<ILevelService>();
            ILevelData[] levelsData = levelService.ReadLevelsData();

            Assert.IsNotNull(levelsData);
            Assert.IsTrue(levelsData.Length > 0);
        }

        [Test]
        public void TestTotalAstroidsOfLevel()
        {
            ILevelService levelService = ServiceHolder.ServiceProvider.GetService<ILevelService>();

            Assert.AreEqual(1, levelService.CalculateTotalAstroids(0, 0, 1));
            Assert.AreEqual(5, levelService.CalculateTotalAstroids(0, 1, 1));
            Assert.AreEqual(14, levelService.CalculateTotalAstroids(1, 1, 1));
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}