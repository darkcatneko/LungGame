using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using Game.UI;
using Gamemanager;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStatus status_;
    [SerializeField] Rigidbody playerRigidbody_;
    [SerializeField] int basicSpeed_;
    [SerializeField] Vector2 playerDirection_ = new Vector2(0, 1f);

    [SerializeField] float hurtTime_;
    
    [SerializeField] AudioData playerHurt_audioData_;

    [SerializeField] private bool playerBGMOpen;
    
    [SerializeField] private GameObject breathingNomalMusicPlayer;
    [SerializeField] private GameObject runMusicPlayer;
    [SerializeField] Animator camAnimator_;
    [SerializeField] private Sprite[] leftHandLife;
     [SerializeField] private Sprite[] rightHandLife;
    [SerializeField] SpriteRenderer leftHandSprite;
    [SerializeField] SpriteRenderer rightHandSprite;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject trapController;
    [SerializeField] AudioListener audioListener;
    private int hurtCount;
    private void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerMove, cmd => { playerDirection_ = cmd.Input; });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerHurt, cmd => { PlayerHurt(); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.TriggerWallShrink, cmd => { PlayerCameraShaking(); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnCallCamShake, cmd => { PlayerCameraShaking(); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnSetPlayerBGM, cmd => { playerBGMOpen = cmd.SetPlayerBGMOpen; });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnStartGame, cmd => { status_ = PlayerStatus.Run; camAnimator_.SetBool("GameStart", true); });
        hurtCount=0;
    }

    private void Update()
    {
        if (playerBGMOpen)
        {
            breathingNomalMusicPlayer.SetActive(true);
            runMusicPlayer.SetActive(true);
        }
        else
        {
            breathingNomalMusicPlayer.SetActive(false);
            runMusicPlayer.SetActive(false);
        }
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

    private void PlayerCameraShaking()
    {
        camAnimator_.SetTrigger("Shaking");
    }
    
    private void PlayerHurt()
    {
        if (status_ != PlayerStatus.Hurt) // 避免重複觸發
        {
            StartCoroutine(SwitchToHurt());
        }
    }

    private IEnumerator SwitchToHurt()
    {
        status_ = PlayerStatus.Hurt;
        
        GameManager.Instance.MainGameEvent.Send(new SetPlayerBGM() {SetPlayerBGMOpen = false });    
        AudioManager.Instance.PlayRandomSFX(playerHurt_audioData_);
        UIManager.Instance.OpenPanel(UIType.GameBlood_TransitionPanel);
        yield return new WaitForSeconds(hurtTime_); // 等待2秒
        status_ = PlayerStatus.Run;
        GameManager.Instance.MainGameEvent.Send(new SetPlayerBGM() {SetPlayerBGMOpen = true });
        hurtCount++;
        leftHandSprite.sprite=leftHandLife[hurtCount];
        rightHandSprite.sprite=rightHandLife[hurtCount];
        if(hurtCount==2)
        {
            playerRigidbody_.drag = 48f;
            Invoke("GameOver", 5f);
        }
        
    }

    void GameOver(){
        playerRigidbody_.drag = 50f;
        camAnimator_.enabled = false;
        gameOver.SetActive(true);
        trapController.SetActive(false);
        GameManager.Instance.MainGameEvent.Send(new SetPlayerBGM() {SetPlayerBGMOpen = false }); 
    }
}

public enum PlayerStatus
{
    Run,
    Hurt,
    Prepare,
}