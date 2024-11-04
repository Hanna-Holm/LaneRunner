using LaneRunner.Collisions;
using LaneRunner.Collisions.CollisionDetection;
using LaneRunner.Lanes;
using LaneRunner.Players;
using LaneRunner.Players.PlayerMechanisms;

namespace LaneRunner
{
    internal class GameSetup
    {
        public int WindowHeight { get; }
        public int WindowWidth { get; }
        public Lane[] Lanes { get; } = new Lane[2];
        public int LaneColumns { get; } = 7;
        public int LaneRows { get; } = 16;
        private readonly int _OriginY = 20;
        private readonly int _firstLaneOriginX;
        private readonly int _secondLaneOriginX;

        public GameSetup(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            _firstLaneOriginX = windowWidth / 10;
            _secondLaneOriginX = windowWidth - windowWidth / 3;

            SetupLanes();
        }

        private void SetupLanes()
        {
            var humanPlayer = new Player(new HumanPlayMechanism(), new Collider(new CellCollisionDetector()));
            Lanes[0] = new Lane(humanPlayer, LaneColumns, LaneRows, _firstLaneOriginX, _OriginY);
            var computerPlayer = new Player(new ComputerPlayMechanism(), new Collider(new CellCollisionDetector()));
            Lanes[1] = new Lane(computerPlayer, LaneColumns, LaneRows, _secondLaneOriginX, _OriginY);
        }
    }
}
