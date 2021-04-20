namespace RNMessageSystem.ToRNMessages
{
    public class LongPressData : ToRnMessageData
    {
        public string InputType { get; } = "LongPress";
        public string TargetObject;
    }
}