namespace RNMessageSystem.ToRNMessages
{
    public class ClickData : ToRnMessageData
    {
        public string InputType { get; } = "Click";
        public string TargetObject;
    }
}