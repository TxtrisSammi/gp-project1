using UnityEngine;
    public class GameManager : Singleton<GameManager>
    {
        // Game state variables
        private bool _isGameActive;
        private float _gameTime;
        
        // Methods go here
        public override void Awake()
        {
            // Call Singleton Awake
            base.Awake();
        }

        void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            Debug.Log("[GameManager] Initializing...");
            _isGameActive = false;
            _gameTime = 0f;
        }

        public void StartGame()
        {
            Debug.Log("[GameManager] Game Started!");
            _isGameActive = true;
        }

        public void PauseGame()
        {
            Debug.Log("[GameManager] Game Paused!");
            
            _isGameActive = false;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Debug.Log("[GameManager] Game Resumed!");
            
            _isGameActive = true;
            Time.timeScale = 1f;
        }

        void Update()
        {
            if (_isGameActive)
            {
                _gameTime += Time.deltaTime;
            }
        }
    }