using System.Collections.Generic;

namespace RNMessageSystem
{
    public class RnMessage
    {
        public string Controller;
        public string Action;
        public List<KeyValuePair<string,string>> Params;
    }
}