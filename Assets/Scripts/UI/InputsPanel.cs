using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using UnityEngine;

namespace UI
{
    public class InputsPanel : MonoBehaviour
    {
        [SerializeField] private ClickAndHoldButton _leftButton;
        [SerializeField] private ClickAndHoldButton _rightButton;
        [SerializeField] private ClickAndHoldButton _upButton;
        [SerializeField] private ClickAndHoldButton _shootButton;
        private IEventService _eventService;

        private void Awake()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void Start()
        {
            SetButtonsListener();
        }

        private void SetButtonsListener()
        {
            _leftButton.SetHoldListener(() => _eventService.BroadcastEvent(EventTypes.OnUserClickedSides, Vector2.left));
            _rightButton.SetHoldListener(() => _eventService.BroadcastEvent(EventTypes.OnUserClickedSides, Vector2.right));
            _upButton.SetHoldListener(() => _eventService.BroadcastEvent(EventTypes.OnUserClickedForward));
            _shootButton.SetHoldListener(() => _eventService.BroadcastEvent(EventTypes.OnUserShot));
        }
    }
}