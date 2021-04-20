using System;
using System.Collections.Generic;
using System.Reflection;

namespace RNMessageSystem.ToUnityMessages.Controllers
{
    public abstract class RnMessageController : IRnMessageController
    {
        private readonly Dictionary<string, Delegate> _actions;

        protected RnMessageController()
        {
            _actions = new Dictionary<string, Delegate>();
            var type = GetType();
            var methods = type.GetMethods(  BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                var actionAttribute = method.GetCustomAttribute<ActionName>();

                if (actionAttribute != null)
                {
                    Delegate action = Delegate.CreateDelegate(typeof(Action<string>), this, method);
                    _actions.Add(actionAttribute.Name, action);
                }
            }
        }

        public void InvokeAction(string action, string parameters)
        {
            if (_actions.ContainsKey(action))
            {
                _actions[action].DynamicInvoke(parameters);
            }
            else
            {
                throw new Exception("There is no actions with name = " + action);
            }
        }
    }
}