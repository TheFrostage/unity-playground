using System;

namespace RNMessageSystem
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