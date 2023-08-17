using SFML.Graphics;

namespace The_Relic
{
    class StatesController
    {
        GameOverState gameOverState;
        VictoryState victoryState;

        InfoState infoState;

        private RenderWindow window;
        private MainMenuState mainMenuState;
        private GameState gameState;
        private OptionsMenuState optionsMenuState;
        private SoundMenuState soundMenuState;
        private ControllsMenuState controllsMenuState;
        private CreditsMenuStates creditsMenuStates;

        private InGameMenuState inGameMenuState;
        private InGameOptionsMenuState inGameOptionsMenuState;
        private InGameSoundMenuState inGameSoundMenuState;
        private InGameControllsMenuState inGameControllsMenuState;

        public StatesController(RenderWindow window)
        {
            this.window = window;

            gameOverState = new GameOverState(window);
            victoryState = new VictoryState(window);

            infoState = new InfoState(window);

            mainMenuState = new MainMenuState(window);
            gameState = new GameState(window);
            optionsMenuState = new OptionsMenuState(window);
            soundMenuState = new SoundMenuState(window);
            controllsMenuState = new ControllsMenuState(window);
            creditsMenuStates = new CreditsMenuStates(window);

            inGameMenuState = new InGameMenuState(window);
            inGameOptionsMenuState = new InGameOptionsMenuState(window);
            inGameSoundMenuState = new InGameSoundMenuState(window);
            inGameControllsMenuState = new InGameControllsMenuState(window);

            gameOverState.OnMenuPressed += MainMenu;
            gameOverState.OnRetryPressed += StartGame;
            victoryState.OnMenuPressed += MainMenu;
            victoryState.OnRetryPressed += StartGame;

            mainMenuState.OnPlayPressed += StartGame;
            mainMenuState.OnOptionsPressed += OptionsMenu;
            mainMenuState.OnCreditsPressed += CreditsMenu;
            mainMenuState.OnQuitPressed += QuitApplication;

            optionsMenuState.OnSoundPressed += SoundMenu;
            optionsMenuState.OnControllsPressed += ControllsMenu;
            optionsMenuState.OnBackPressed += MainMenu;

            soundMenuState.OnBackPressed += OptionsMenu;

            controllsMenuState.OnBackPressed += OptionsMenu;

            creditsMenuStates.OnBackPressed += MainMenu;

            gameState.GameMenu += InGameMenu;
            gameState.OnDeath += GameOver;
            gameState.OnWin += Victory;

            infoState.OnOkPressed += StartGameInfo;

            inGameMenuState.OnResumePressed += ResumeGame;
            inGameMenuState.OnOptionsPressed += InGameOptions;
            inGameMenuState.OnMainMenuPressed += MainMenu;
            inGameMenuState.OnQuitPressed += QuitApplication;

            inGameOptionsMenuState.OnSoundPressed += InGameSound;
            inGameOptionsMenuState.OnControllsPressed += InGameControlls;
            inGameOptionsMenuState.OnBackPressed += InGameMenu;

            inGameSoundMenuState.OnBackPressed += InGameOptions;
            inGameControllsMenuState.OnBackPressed += InGameOptions;

            window.SetMouseCursorVisible(false);
            AudioManager.MainMenuMusic.Play();
            mainMenuState.Play();
        }

        // =========================================== In Game Menus ======================================================
        private void InGameMenu()
        {
            gameState.Pause();
            inGameOptionsMenuState.Stop();
            inGameMenuState.Play();
        }
        private void ResumeGame()
        {
            Cursor.EnableCross(true);
            inGameMenuState.ResetBackGround();

            gameState.Resume();
            inGameMenuState.Stop();
        }
        private void InGameOptions()
        {
            inGameMenuState.Stop();
            inGameSoundMenuState.Stop();
            inGameControllsMenuState.Stop();

            inGameOptionsMenuState.SetBackGround(inGameMenuState.BackGround);
            inGameOptionsMenuState.Play();
        }
        private void InGameSound()
        {
            inGameOptionsMenuState.Stop();

            inGameSoundMenuState.SetBackGround(inGameMenuState.BackGround);
            inGameSoundMenuState.Play();
        }
        private void InGameControlls()
        {
            inGameOptionsMenuState.Stop();

            inGameControllsMenuState.SetBackGround(inGameMenuState.BackGround);
            inGameControllsMenuState.Play();
        }
        // =========================================== In Game Menus ======================================================

        private void StartGame()
        {
            AudioManager.MainMenuMusic.Stop();
            mainMenuState.Stop();
            victoryState.Stop();
            gameOverState.Stop();

            if (gameState.firstStart == true)
                infoState.Play();
            else gameState.Play();
        }
        private void StartGameInfo()
        {
            infoState.Stop();
            gameState.Play();
        }

        private void GameOver()
        {
            gameState.Stop();
            gameOverState.Play();
        }
        private void Victory()
        {
            gameState.Stop();
            victoryState.Play();
        }

        private void MainMenu()
        {
            gameState.Stop();
            optionsMenuState.Stop();
            creditsMenuStates.Stop();
            victoryState.Stop();
            gameOverState.Stop();

            inGameMenuState.Stop();

            mainMenuState.Play();
        }
        private void OptionsMenu()
        {
            mainMenuState.Stop();
            soundMenuState.Stop();
            controllsMenuState.Stop();
            optionsMenuState.Play();
        }
        private void SoundMenu()
        {
            optionsMenuState.Stop();
            soundMenuState.Play();
        }
        private void ControllsMenu()
        {
            optionsMenuState.Stop();
            controllsMenuState.Play();

        }
        private void CreditsMenu()
        {
            mainMenuState.Stop();
            creditsMenuStates.Play();

        }

        private void QuitApplication()
        {
            mainMenuState.Stop();
            gameState.Stop();

            inGameMenuState.Stop();

            window.Close();
        }
    }
}
