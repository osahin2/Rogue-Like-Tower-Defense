using System;

namespace Rogue.GameEvent
{
    public class GameplayEvents
    {
        public const string GAME_END = "GameEnd";

        public class GameEnd : EventArgs { }
    }
}