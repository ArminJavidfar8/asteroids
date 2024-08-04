using Services.UpdateService.Abstraction;
using UnityEngine;

namespace Services.UpdateService
{
    public class UpdateHolder : MonoBehaviour
    {
        private IUpdateService _updateService;

        public void SetUpdateListener(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Update()
        {
            _updateService.OnUpdate();
        }

        private void LateUpdate()
        {
            _updateService.OnLateUpdate();
        }

        private void FixedUpdate()
        {
            _updateService.OnFixedUpdate();
        }
    }
}