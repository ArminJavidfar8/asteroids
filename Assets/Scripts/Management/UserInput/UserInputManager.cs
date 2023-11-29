using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Management.UserInput
{
    public class UserInputManager : MonoBehaviour
    {
        private IEventService _eventService;

        private void Start()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void Update()
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
        }
    }
}