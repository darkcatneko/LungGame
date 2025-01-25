using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using Game.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStatus status_;
    [SerializeField] Rigidbody playerRigidbody_;
    [SerializeField] int basicSpeed_;
    [SerializeField] Vector2 playerDirection_ = new Vector2(0, 1f);

    [SerializeField] float hurtTime_;
    
    [SerializeField] AudioData enemyDeath_audioData_;
    [SerializeField] AudioData playerHurt_audioData_;
    private void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerMove, cmd => { playerDirection_ = cmd.Input; });
    }

    private void FixedUpdate()
    {
        if (status_ == PlayerStatus.Run)
        {
            var dir = new Vector3(playerDirection_.x, 0, playerDirection_.y);
            playerRigidbody_.velocity = basicSpeed_ * dir;
        }
        else if (status_ == PlayerStatus.Hurt)
        {
            var dir = new Vector3(playerDirection_.x, 0, playerDirection_.y);
            playerRigidbody_.velocity = basicSpeed_ * dir / 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.transform.parent.gameObject);
            if (status_ != PlayerStatus.Hurt) // 避免重複觸發
            {
                StartCoroutine(SwitchToHurt());
            }
        }
    }

    private IEnumerator SwitchToHurt()
    {
        status_ = PlayerStatus.Hurt;
        AudioManager.Instance.PlayRandomSFX(enemyDeath_audioData_);
        AudioManager.Instance.PlayRandomSFX(playerHurt_audioData_);
        UIManager.Instance.OpenPanel(UIType.GameBlood_TransitionPanel);
        yield return new WaitForSeconds(hurtTime_); // 等待2秒
        status_ = PlayerStatus.Run;
    }
}

public enum PlayerStatus
{
    Run,
    Hurt,
}