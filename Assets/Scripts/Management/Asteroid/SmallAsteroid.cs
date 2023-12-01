using Services.Abstraction;

namespace Management.Asteroid
{
    public class SmallAsteroid : BaseAsteroid
    {
        public override void Die()
        {
            _asteroidsService.RemoveAsteroid(this);
        }
    }
}