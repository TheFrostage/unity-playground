using System;

namespace RNMessageSystem.ToUnityMessages
{
    public class ControllerNameAttribute : Attribute
    {
        public string Name;

        public ControllerNameAttribute(string name)
        {
            Name = name;
        }
    }
}