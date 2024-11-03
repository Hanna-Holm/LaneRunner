using LaneRunner.Lanes;
using LaneRunner.UI.Rendering;
using Raylib_cs;

namespace LaneRunner
{
    internal class Game
    {
        private int _windowWidth = 1200;
        private readonly int _windowHeight = 700;
        private readonly int _framesPerSecond = 60;
        private readonly string _gameTitle = "LaneRunner";
        public GameSetup GameContext { get; }
        private Lane _firstLane;
        private Lane _secondLane;
        private float timer;
        private string _computerWinsMsg = "Computer wins! Press <Esc> to close window.";
        private string _humanWinsMsg = "Congratulations! You win! Press <Esc> to close window.";

        public Game()
        {
            GameContext = new GameSetup(_windowWidth, _windowHeight);
            _firstLane = GameContext.Lanes[0];
            _secondLane = GameContext.Lanes[1];
        }

        public void Run()
        {
            Raylib.InitWindow(_windowWidth, _windowHeight, _gameTitle);
            var laneRenderer = new LaneRenderer();
            laneRenderer.InitializeTextures();
            Raylib.SetTargetFPS(_framesPerSecond);

            bool gameOver = false;
            string gameOverMessage = "";

            while (!Raylib.WindowShouldClose())
            {
                timer = Raylib.GetFrameTime();
                _firstLane.Update(timer);
                _secondLane.Update(timer);

                if (_firstLane.Player.Health <= 0)
                {
                    gameOver = true;
                    gameOverMessage = _computerWinsMsg;
                }
                else if (_secondLane.Player.Health <= 0)
                {
                    gameOver = true;
                    gameOverMessage = _humanWinsMsg;
                }
                else
                {
                    laneRenderer.Render(_firstLane);
                    laneRenderer.Render(_secondLane);
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                if (gameOver)
                {
                    Raylib.DrawText(gameOverMessage, _windowWidth / 5, _windowHeight / 3, 30, Color.Black);
                }

                Raylib.EndDrawing();
            }

            laneRenderer.UnloadTextures();
            Raylib.CloseWindow();
        }
    }
}
