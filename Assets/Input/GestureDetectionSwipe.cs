namespace PH.Input
{
    using UnityEngine;

    public class GestureDetectionSwipe
    {
        private const float MinimumSwipeDistance = 30f;
        private const float MinGestureDuration = 0.05f;
        private const float MaxGestureDuration = 0.25f;

        public enum SwipeGestures
        {
            Invalid,
            SwipeUp,
            SwipeDown,
            SwipeLeft,
            SwipeRight
        }

        public SwipeGestures CalculateSwipe(InputTouchContact contactStart, InputTouchContact contactEnd)
        {
            var direction = contactEnd.Position - contactStart.Position;
            var distance = Vector2.Distance(contactStart.Position, contactEnd.Position);
            var duration = contactEnd.Time - contactStart.Time;
            
            // Invalid gesture if not a minimum distance or duration.
            if (distance < MinimumSwipeDistance) return SwipeGestures.Invalid;
            if (duration < MinGestureDuration) return SwipeGestures.Invalid;
            if (duration > MaxGestureDuration) return SwipeGestures.Invalid;

            var horizontalMagnitude = Mathf.Abs(direction.x);
            var verticalMagnitude = Mathf.Abs(direction.y);

            // If we swiped horizontally more than vertically, evaluate for Right/Left..
            if (horizontalMagnitude > verticalMagnitude)
            {
                if (direction.x > 0)
                    return SwipeGestures.SwipeRight;
                return SwipeGestures.SwipeLeft;
            }

            // ..otherwise evaluate for Up/Down
            if (direction.y > 0)
                return SwipeGestures.SwipeUp;
            return SwipeGestures.SwipeDown;
        }
    }
}