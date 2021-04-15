using System;

namespace RNMessageSystem
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