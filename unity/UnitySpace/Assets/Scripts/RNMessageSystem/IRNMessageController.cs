using System.Collections.Generic;

namespace RNMessageSystem
{
    public interface IRnMessageController
    {
        void InvokeAction(string action, string parameters);
    }
}