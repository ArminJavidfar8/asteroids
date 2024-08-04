using Services.Abstraction.Spaceship;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;

namespace Management.Level
{
    public class LevelManager
    {
        private ISpaceshipService _spaceshipService;
        private IEventService _eventService;

        public LevelManager(ISpaceshipService spaceshipService, IEventService eventService )
        {
            _spaceshipService = spaceshipService;
            _eventService = eventService;

            _eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
        }

        private void LevelStarted()
        {
            _spaceshipService.CreatePlayer();
        }
    }
}