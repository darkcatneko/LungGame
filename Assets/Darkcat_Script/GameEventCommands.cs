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

}
