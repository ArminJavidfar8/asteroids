using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.UpdateService.Abstraction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Management.UserInput
{
    public class UserInputManager
    {
        private IEventService _eventService;

        public UserInputManager(IEventService eventService, IUpdateService updateService)
        {
            _eventService = eventService;
            updateService.RegisterUpdate(OnUpdate);
        }

        private void OnUpdate()
        {
            if (Keyboard.current.wKey.IsPressed())
            {
                _eventService.BroadcastEvent(EventTypes.OnUserClickedForward);
            }
            if (Keyboard.current.aKey.IsPressed())
            {
                _eventService.BroadcastEvent(EventTypes.OnUserClickedSides, Vector2.left);
            }
            if (Keyboard.current.dKey.IsPressed())
            {
                _eventService.BroadcastEvent(EventTypes.OnUserClickedSides, Vector2.right);
            }
            if (Keyboard.current.spaceKey.isPressed)
            {
                _eventService.BroadcastEvent(EventTypes.OnUserShot);
            }
        }
    }
}