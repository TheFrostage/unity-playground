using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RNMessageSystem.Controllers;

namespace RNMessageSystem
{
    public class MessagesSystem
    {
        private Dictionary<string, RnMessageController> _controllers;

        public void Init()
        {
            UnityMessageManager.Instance.OnMessage += OnRNMessage;
            InitControllers();
        }

        private void InitControllers()
        {
            _controllers = new Dictionary<string, RnMessageController>();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetInterface(nameof(IRnMessageController)) != null)
                {
                    var controllerNameAttribute = type.GetCustomAttribute<ControllerNameAttribute>();

                    if (controllerNameAttribute != null)
                    {
                        _controllers.Add(controllerNameAttribute.Name, (RnMessageController) Activator.CreateInstance(type));
                    }
                }
            }

            //OnRNMessage(@"{""Controller"" : ""Scene"", ""Action"" : ""Init"", ""Params"" : ""{test}""}");
        }

        private void OnRNMessage(string message)
        {
            if (message != "")
            {
                var json = JObject.Parse(message);
                var controller = json["Controller"].ToString();
                var action = json["Action"].ToString();
                var parameters = json["Params"].ToString();

                if (_controllers.ContainsKey(controller))
                {
                    _controllers[controller].InvokeAction(action, parameters);
                }
                else
                {
                    throw new Exception("There is no controller with name " + controller);
                }
            }
        }

        public void Deinit()
        {
            UnityMessageManager.Instance.OnMessage -= OnRNMessage;
        }
    }
}