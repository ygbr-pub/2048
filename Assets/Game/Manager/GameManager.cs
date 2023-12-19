namespace PH.Game
{
    /* TODO List:
    - Tapping on tiles is soundboard/springy. Just a fun easter-egg I suppose.
    - Prefab-ify scene objects
    - Keyboard Input
    - CloudKit: KVS Score Saving/Reading
    - Find + Add Audio Effects
    - Haptics
    - Title Screen
        - Title
        - Play / Settings (Cog) / Achievements (Trophy)
    - Game Screen
        - Re-arranged
        - Settings (Cog)
        - Achievement (Trophy)
    - Settings Screen
        - Mute
        - Haptics
        - Active Theme
        - Dark Mode Theme
        - Low Battery Mode Enabled/Disabled
            - Reduce Framerate (30)
            - Disable AA
            - Replace Procedural image with rasterized 9-slice sprite
        - Reset High Score
    - Confirm Modal UIs
    - Achievement Screen
        - Achievements list in scroll view?
    - Juice:
        - Text Animator
        - Better Transitions
        - Blurs?
        - Particles?
    - Object Pooling
    - Abstract away a lot of core systems for reuse
    */
    
    /* Licensed Assets
        - DoTween Pro           -> PrimeTween? Maybe a no-alloc tween library
        - AudioSFX              -> Swap or self-produce
        - Procedural Image      -> 9-Slice Sprites
        - CloudKit              -> Strip Functionality
        - uPalette              -> Develop our own fork
    */

    using System;
    using System.Globalization;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    
    public class GameManager : MonoBehaviour
    {
        private const string HighScoreKey = "highscore";

        public static GameManager Instance { get; private set; }
        public static Action<int> OnHighScore;

        private int Score { get; set; }
        
        private CloudKitManager _cloudKit;
        [SerializeField] private TileBoard board;
        [SerializeField] private CanvasGroup gameOver;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI hiscoreText;
        
        private void Awake()
        {
            if (Instance != null) 
                DestroyImmediate(gameObject);
            else 
                Instance = this;

            if (Instance == this)
                DontDestroyOnLoad(gameObject);

#if UNITY_IOS
            _cloudKit = gameObject.AddComponent<CloudKitManager>();
#endif
        }

        private void Start()
        {
            NewGame();
        }

        public void NewGame()
        {
            SetScore(0);
            var highScore = LoadHighScore();
            hiscoreText.text = highScore.ToString();

            HideGameOver();
            board.ClearBoard();

            var spawnSeq = DOTween.Sequence();
            spawnSeq.AppendInterval(0.2f);
            spawnSeq.AppendCallback(SpawnTile);
            spawnSeq.AppendInterval(0.2f);
            spawnSeq.AppendCallback(SpawnTile);
            spawnSeq.OnComplete(OnSpawnSequenceCompleted);

            void SpawnTile() => board.CreateTile();

            void OnSpawnSequenceCompleted() => board.enabled = true;
        }

        private void HideGameOver()
        {
            gameOver.alpha = 0f;
            gameOver.interactable = false;
        }

        public void ShowGameOver()
        {
            board.enabled = false;
            gameOver.interactable = true;
            FadeInGameOverScreen();

            void FadeInGameOverScreen()
            {
                const float delay = 1f;
                const float duration = 0.5f;
                var gameOverFadeSeq = DOTween.Sequence();
                gameOverFadeSeq.SetDelay(delay);
                gameOverFadeSeq.Append(gameOver.DOFade(1f, duration));
            }
        }

        public void IncreaseScore(int points)
        {
            SetScore(Score + points);
        }

        private void SetScore(int score)
        {
            var prevScore = Score;
            Score = score;
            SaveHighScore();
            
            var tweenId = GetInstanceID();
            DOTween.Kill(tweenId);
            
            const float duration = 0.5f;
            var counter = (float) Score;
            DOTween.To(UpdateAnimCounter, prevScore, score, duration)
                .OnUpdate(UpdateLabelText)
                .OnComplete(FinalizeLabelText)
                .SetId(tweenId);                
                
            void UpdateAnimCounter(float x) => counter = x;
            void UpdateLabelText() => scoreText.text = Mathf.RoundToInt(counter).ToString(CultureInfo.InvariantCulture);
            void FinalizeLabelText() => scoreText.text = Score.ToString();
        }

        private void SaveHighScore()
        {
            var highScore = LoadHighScore();
            
            if (Score <= highScore) 
                return;
            
            PlayerPrefs.SetInt(HighScoreKey, Score);
            OnHighScore?.Invoke(highScore);
        }


        private int LoadHighScore()
        {
            var localHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
            var remoteHighScore = 0;
            
#if UNITY_IOS
            remoteHighScore = _cloudKit.GetHighScore();
#endif

            return Mathf.Max(localHighScore, remoteHighScore);
        }

    }
    
}