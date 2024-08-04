using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.UpdateService.Abstraction
{
    public class UpdateService : IUpdateService
    {
        private UpdateHolder _updateHolder;
        private List<Action> _updateActions;
        private List<Action> _fixedupdateActions;
        private List<Action> _lateUpdateActions;
        private int _updateActionsCount;
        private int _fixedUpdateActionsCount;
        private int _lateUpdateActionsCount;

        public UpdateService()
        {
            _updateHolder = new GameObject().AddComponent<UpdateHolder>();
            _updateHolder.SetUpdateListener(this);
            _updateActions = new List<Action>();
        }

        public void OnUpdate()
        {
            for (int i = 0; i < _updateActionsCount; ++i)
            {
                _updateActions[i]?.Invoke();
            }
        }

        public void OnFixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateActionsCount; ++i)
            {
                _fixedupdateActions[i]?.Invoke();
            }
        }

        public void OnLateUpdate()
        {
            for (int i = 0; i < _lateUpdateActionsCount; ++i)
            {
                _lateUpdateActions[i]?.Invoke();
            }
        }

        public void RegisterUpdate(Action action)
        {
            _updateActions.Add(action);
            ++_updateActionsCount;
        }

        public void UnRegisterUpdate(Action action)
        {
            if (_updateActions.Remove(action))
            {
                --_updateActionsCount;
            }
        }

        public void RegisterFixedUpdate(Action action)
        {
            _fixedupdateActions.Add(action);
            ++_fixedUpdateActionsCount;
        }

        public void UnRegisterFixedUpdate(Action action)
        {
            if (_fixedupdateActions.Remove(action))
            {
                --_fixedUpdateActionsCount;
            }
        }

        public void RegisterLateUpdate(Action action)
        {
            _lateUpdateActions.Add(action);
            ++_lateUpdateActionsCount;
        }

        public void UnRegisterLateUpdate(Action action)
        {
            if (_lateUpdateActions.Remove(action))
            {
                --_lateUpdateActionsCount;
            }
        }
    }
}