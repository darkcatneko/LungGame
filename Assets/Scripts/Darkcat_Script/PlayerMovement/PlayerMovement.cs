using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStatus status_;
    [SerializeField] Rigidbody playerRigidbody_;
    [SerializeField] int basicSpeed_;
    [SerializeField] Vector2 playerDirection_ = new Vector2(0,1f);
    private void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerMove, cmd => { playerDirection_ = cmd.Input; });
    }
    private void FixedUpdate()
    {
        if (status_ == PlayerStatus.Run)
        {
            var dir = new Vector3(playerDirection_.x, 0, playerDirection_.y);
            playerRigidbody_.velocity = basicSpeed_* dir;
        }
    }
}

public enum PlayerStatus
{
    Run,
}