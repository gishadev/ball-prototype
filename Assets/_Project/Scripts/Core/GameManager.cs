using System.Collections.Generic;
using Gisha.BallGame.Ball;
using Gisha.BallGame.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.BallGame.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private static GameManager _gameManager;

        public static GameManager Instance
        {
            get
            {
                if (!_gameManager)
                {
                    _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                    if (!_gameManager)
                        Debug.LogError("No one active GameManager script in your scene.");
                    else
                        DontDestroyOnLoad(_gameManager);
                }

                return _gameManager;
            }
        }

        public IWorldTouchController WorldTouchController { get; private set; }
        public PlayerBall PlayerBall { get; private set; }
        public PathBuilder PathBuilder { get; private set; }
        public ScoreManager ScoreManager { get; private set; }

        public bool IsLose => _isLose;
        public bool IsWin => _isWin;

        private bool _isWin, _isLose;

        private void Awake()
        {
            WorldTouchController = new WorldTouchController();

            EventManager.StartListening(Constants.EVENT_NEAR_TROPHY, Win);
            EventManager.StartListening(Constants.EVENT_MASS_ZERO, Lose);
            SceneManager.sceneLoaded += OnSceneLoaded;
            ScoreManager = new ScoreManager();
            
            Init();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            Init();
        }

        private void Init()
        {
            PlayerBall = Instantiate(playerPrefab).GetComponent<PlayerBall>();
            PathBuilder = FindObjectOfType<PathBuilder>();
        }

        private void OnDisable()
        {
            WorldTouchController.Dispose();

            EventManager.StopListening(Constants.EVENT_NEAR_TROPHY, Win);
            EventManager.StopListening(Constants.EVENT_MASS_ZERO, Lose);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Update()
        {
            WorldTouchController.Tick();
        }

        private void Win(Dictionary<string, object> dictionary)
        {
            if (IsWin || IsLose)
                return;

            Debug.Log("You win!");
            EventManager.TriggerEvent(Constants.EVENT_WIN, null);
            _isLose = false;
            _isWin = true;
        }

        private void Lose(Dictionary<string, object> obj)
        {
            if (IsWin || IsLose)
                return;

            Debug.Log("You lose!");
            EventManager.TriggerEvent(Constants.EVENT_LOSE, null);
            _isWin = false;
            _isLose = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
            _isLose = false;
            _isWin = false;
        }
    }
}