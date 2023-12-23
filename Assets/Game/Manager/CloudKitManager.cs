namespace PH.Game
{
    using UnityEngine;
    using HovelHouse.CloudKit;

    public class CloudKitManager : MonoBehaviour
     {
         private NSUbiquitousKeyValueStore _kvs;
         private const string HighScoreKey = "HighScore";

         private void Awake()
         {
         }

         private void Start()
        {
            _kvs = NSUbiquitousKeyValueStore.DefaultStore;
            _kvs.AddDidChangeExternallyNotificationObserver(OnKvsChanged);
        }

        private void OnKvsChanged(long arg1, string[] arg2)
        {
            Debug.Log($"Keystore has changed keys: {arg1} | {string.Join(",", arg2)})");
            foreach(var str in arg2)
            {
                object val = str switch
                {
                    HighScoreKey => _kvs.DoubleForKey(str),
                    _ => null
                };
                Debug.Log($"{str}:{val}");
            }
        }

        private void SetHighScore(int score)
        {
            _kvs.SetDouble(score, HighScoreKey);
            _kvs.Synchronize();
        }

        public int GetHighScore()
        {
            var highScore = _kvs.DoubleForKey(HighScoreKey);
            return (int) highScore;
        }
    }
}
