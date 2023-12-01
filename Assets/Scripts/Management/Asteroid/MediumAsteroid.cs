using Management.Abstraction;
using Services.Abstraction;

namespace Management.Asteroid
{
    public class MediumAsteroid : BaseAsteroid
    {
        public override void Die()
        {
            for (int i = 0; i < 3; i++)
            {
                IAsteroid asteroid = _asteroidsService.AddAsteroid(AsteroidType.Small);
                asteroid.Stop();
                asteroid.SetPosition(Position);
                asteroid.MoveProperly();
                _asteroidsService.RemoveAsteroid(this);
            }
        }
    }
}