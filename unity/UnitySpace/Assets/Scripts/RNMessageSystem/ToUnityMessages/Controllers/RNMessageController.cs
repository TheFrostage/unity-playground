using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RNMessageSystem.ToUnityMessages.Controllers
{
    public abstract class RnMessageController : IRnMessageController
    {
        protected RnMessageController()
        {
            _actions = new Dictionary<string, Action<string>>();
        }
        protected readonly Dictionary<string, Action<string>> _actions;

        public void InvokeAction(string action, string parameters)
        {
            if (_actions.ContainsKey(action))
            {
                _actions[action]?.Invoke(parameters);
            }
            else
            {
                throw new Exception("There is no actions with name = " + action);
            }
        }
    }
}