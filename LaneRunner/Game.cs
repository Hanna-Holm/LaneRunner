using LaneRunner.Lanes;
using LaneRunner.UI.Rendering;
using Raylib_cs;

namespace LaneRunner
{
    internal class Game
    {
        private int _windowWidth = 1100;
        private readonly int _windowHeight = 700;
        private readonly int _framesPerSecond = 60;
        private readonly string _gameTitle = "LaneRunner";
        public GameSetup GameContext { get; }
        private Lane _firstLane;
        private Lane _secondLane;
        private float timer;

        public Game()
        {
            GameContext = new GameSetup(_windowWidth, _windowHeight);
            _firstLane = GameContext.Lanes[0];
            _secondLane = GameContext.Lanes[1];
        }

        public void Run()
        {
            // I got the methods InitWindow(width, height, title), the while-loop with
            // !Raylib.WindowShouldClode() and the methods BeginDrawing(),
            // ClearBackground(), EndDrawing() and CloseWindow() in the README-file
            // included in the folder received after installation (for example at C:/raylib)
            Raylib.InitWindow(_windowWidth, _windowHeight, _gameTitle);
            Raylib.SetTargetFPS(_framesPerSecond);

            while (!Raylib.WindowShouldClose())
            {
                timer = Raylib.GetFrameTime();
                _firstLane.Update(timer);
                if (_firstLane.Player.Health <= 0)
                {
                    Console.WriteLine("Computer wins!");
                    break;
                }
                _secondLane.Update(timer);
                if (_secondLane.Player.Health <= 0)
                {
                    Console.WriteLine("You win!");
                    break;
                }

                // Drawing
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                var laneRenderer = new LaneRenderer();
                laneRenderer.Render(_firstLane);
                laneRenderer.Render(_secondLane);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
