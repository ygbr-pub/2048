namespace PH.Game
{
    /* TODO:
    - App Icon
    - Keyboard Input
    - iOS Deployment
    - KVS Score Saving/Reading
    - Find + Add Audio Effects
    - Title Screen
        - Title
        - Play / Settings (Cog) / Achievements (Trophy)
    - Game Screen
        - Re-arranged
        - Settings (Cog)
        - Achievement (Trophy)
    - Settings Screen
        - Volume Slider
        - Reset High Score
            - Confirm Modal
        - Low Battery Mode Enabled/Disabled
        - Extra Animations Enabled/Disabled
        - Active Theme
    - Achievement Screen
        - Achievements list in scroll view?
    - Credits (Mail Icon in bottom corner of Title Screen)
    - Juice:
        - Text Animator
        - DoTween Effects
        - Better Transitions
        - Blurs?
        - Particles?
    - Probably replace themes with uPalette package
    */
    
    using System.Collections;
    using TMPro;
    using UnityEngine;
    
    public class GameManager : MonoBehaviour
    {
        private const string HighScoreKey = "highscore";

        public static GameManager Instance { get; private set; }

        [SerializeField] private TileBoard board;
        [SerializeField] private CanvasGroup gameOver;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI hiscoreText;

        private int _score;
        public int Score => _score;

        private void Awake()
        {
            if (Instance != null) 
                DestroyImmediate(gameObject);
            else 
                Instance = this;

            if (Instance == this)
                DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            NewGame();
        }

        public void NewGame()
        {
            SetScore(0);
            var cachedHighScore = LoadHighScore();
            hiscoreText.text = cachedHighScore.ToString();

            HideGameOver();
            board.ClearBoard();
            board.CreateTile();
            board.CreateTile();
            board.enabled = true;
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

            StartCoroutine(Fade(gameOver, 1f, 1f));
        }

        private static IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);

            var elapsed = 0f;
            var duration = 0.5f;
            var from = canvasGroup.alpha;

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = to;
        }

        public void IncreaseScore(int points)
        {
            SetScore(_score + points);
        }

        private void SetScore(int score)
        {
            _score = score;
            scoreText.text = score.ToString();

            SaveHighScore();
        }

        private void SaveHighScore()
        {
            var highScore = LoadHighScore();
            
            if (_score > highScore) 
                PlayerPrefs.SetInt(HighScoreKey, _score);
        }


        private static int LoadHighScore()
        {
            return PlayerPrefs.GetInt(HighScoreKey, 0);
        }

    }
    
}