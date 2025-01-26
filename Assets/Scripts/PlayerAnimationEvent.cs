using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamemanager;

public class PlayerAnimationEvent : MonoBehaviour
{
   public void PlayerStartGame()
    {
        GameManager.Instance.MainGameEvent.Send(new StartCommand());
    }
    public void PlayerCallShake()
    {
        GameManager.Instance.MainGameEvent.Send(new CallCamShake());
    }
}
