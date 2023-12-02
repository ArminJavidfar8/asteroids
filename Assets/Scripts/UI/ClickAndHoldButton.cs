using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ClickAndHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isHeldDown;
        private Action _onHold;

        void Update()
        {
            if (isHeldDown)
                _onHold?.Invoke();
        }

        public void SetHoldListener(Action onHold)
        {
            _onHold = onHold;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isHeldDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHeldDown = false;
        }
    }
}