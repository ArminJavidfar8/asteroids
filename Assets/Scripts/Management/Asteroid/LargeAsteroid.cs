using Management.Abstraction;
using Services.Abstraction;

namespace Management.Asteroid
{
    public class LargeAsteroid : BaseAsteroid
    {
        public override void Die()
        {
            for (int i = 0; i < 2; i++)
            {
                IAsteroid asteroid = _asteroidsService.AddAsteroid(AsteroidType.Medium);
                asteroid.Stop();
                asteroid.SetPosition(Position);
                asteroid.MoveProperly();
            }
            _asteroidsService.RemoveAsteroid(this);
        }
    }
}