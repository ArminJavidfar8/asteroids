using System;

namespace Services.UpdateService.Abstraction
{
    public interface IUpdateService
    {
        void OnUpdate();
        void OnFixedUpdate();
        void OnLateUpdate();
        void RegisterUpdate(Action action);
        void UnRegisterUpdate(Action action);
        void RegisterFixedUpdate(Action action);
        void UnRegisterFixedUpdate(Action action);
        void RegisterLateUpdate(Action action);
        void UnRegisterLateUpdate(Action action);
    }
}