namespace PH.Input
{
    using UnityEngine;

    public struct InputTouchContact
    {
        public InputTouchContact(Vector2 pos, float time)
        {
            Position = pos;
            Time = time;
        }
            
        public readonly Vector2 Position;
        public readonly float Time;
    }
}