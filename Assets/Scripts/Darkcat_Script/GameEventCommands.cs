using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Gamemanager
{
    public class PlayerMoveCommand : GameEventMessageBase
    {
        public Vector2 Input;
    }

    public class WallShrink : GameEventMessageBase
    {
        
    }

    public class PlayerHurt : GameEventMessageBase
    {
        
    }

    public class SetPlayerBGM : GameEventMessageBase
    {
        public bool SetPlayerBGMOpen;
    }
    public class StartCommand:GameEventMessageBase { }

    public class CallCamShake : GameEventMessageBase { }
}
