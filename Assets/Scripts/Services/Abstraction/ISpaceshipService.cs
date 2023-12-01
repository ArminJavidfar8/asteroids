using Management.Abstraction;

namespace Services.Abstraction.Spaceship
{
    public interface ISpaceshipService
    {
        ISpaceshipController Player { get; }
        ISpaceshipController CreatePlayer();
    }
}