using Services.Abstraction;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;

namespace Management.Abstraction
{
    public interface IAsteroid : IPoolable
    {
        IAsteroidData AsteroidData { get; }

        void StartMoving();
        void BreakDown();
    }
}