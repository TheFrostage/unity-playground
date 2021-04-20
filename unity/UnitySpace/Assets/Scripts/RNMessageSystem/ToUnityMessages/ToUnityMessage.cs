using System.Collections.Generic;

namespace RNMessageSystem.ToUnityMessages
{
    public class ToUnityMessage
    {
        public string Controller;
        public string Action;
        public List<KeyValuePair<string,string>> Params;
    }
}