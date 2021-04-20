namespace RNMessageSystem.ToRNMessages
{
    public class ToRnMessage
    {
        public string MessageType;
        public ToRnMessageData Data;

        public ToRnMessage(string messageType, ToRnMessageData data)
        {
            MessageType = messageType;
            Data = data;
        }
    }
}