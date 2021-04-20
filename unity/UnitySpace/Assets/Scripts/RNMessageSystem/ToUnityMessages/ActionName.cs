using System;

namespace RNMessageSystem.ToUnityMessages
{
    public class ActionName : Attribute
    {
        public string Name;

        public ActionName(string name)
        {
            Name = name;
        }
    }
}