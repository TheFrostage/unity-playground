using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RNMessageSystem.ToRNMessages;
using RNMessageSystem.ToUnityMessages;
using RNMessageSystem.ToUnityMessages.Controllers;

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

            OnRNMessage(
                @"{""Controller"" : ""Scene"", ""Action"" : ""Init"",  ""Params"": {""DownloadLink"": ""https://cloudfront.test.marine-snow.co/jpg/{0}.jpg"",
                ""Tracks"": [
                {""id"": ""12a833d5-ef41-4a96-b83b-2644d0d40b8c""},
                {""id"": ""16ac48f2-63f0-4500-9d40-dc1d554b2fdf""},
                {""id"": ""f808bbbf-6113-4b61-b131-bbfd19a4c418""},
                {""id"": ""addbca3b-a586-4aa4-9b83-0f0535d1e92f""},
                {""id"": ""12a833d5-ef41-4a96-b83b-2644d0d40b8c""},
                {""id"": ""16ac48f2-63f0-4500-9d40-dc1d554b2fdf""},
                {""id"": ""f808bbbf-6113-4b61-b131-bbfd19a4c418""},
                {""id"": ""addbca3b-a586-4aa4-9b83-0f0535d1e92f""},
                {""id"": ""12a833d5-ef41-4a96-b83b-2644d0d40b8c""},
                {""id"": ""16ac48f2-63f0-4500-9d40-dc1d554b2fdf""},
                {""id"": ""f808bbbf-6113-4b61-b131-bbfd19a4c418""},
                {""id"": ""addbca3b-a586-4aa4-9b83-0f0535d1e92f""},
                {""id"": ""6ab8b5f7-2eaf-4e88-873f-597a29f88b54""}
                ]}}");
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

        public void SendMessageToRN(string messageType, ToRnMessageData data)
        {
            var toRnMessage = new ToRnMessage(messageType, data);
            string json = JsonConvert.SerializeObject(toRnMessage);
            UnityMessageManager.Instance.SendMessageToRN(json);
        }

        public void Deinit()
        {
            UnityMessageManager.Instance.OnMessage -= OnRNMessage;
        }
    }
}