using UnityEngine;
using DG.Tweening;
using Game.Audio;
using Gamemanager;

public class MonsteBreathrBehavior : MonoBehaviour
{
    [SerializeField] Transform thisTransform_;
    [SerializeField] bool breath_ = false;
    float elapsedTime = 0f;
    [SerializeField] GameObject thin_;
    [SerializeField] GameObject fat_;
    
    [SerializeField] AudioData enemyDeath_audioData_;
    
    [SerializeField] bool noBreath_;
    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnGameOver,
            cmd => { Destroy(this); });
    }

    // Update is called once per frame
    void Update()
    {
        if (noBreath_)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            breath_ = !breath_;
            triggerBreath();
        }
    }
    void triggerBreath()
    {
        if (breath_)
        {
            thisTransform_.DOScaleX(2f, 1f);
            thin_.SetActive(false);
            fat_.SetActive(true);
        }
        else
        {
            thisTransform_.DOScaleX(1f, 1f);
            thin_.SetActive(true);
            fat_.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
            GameManager.Instance.MainGameEvent.Send(new PlayerHurt());
            AudioManager.Instance.PlayRandomSFX(enemyDeath_audioData_);
        }
    }
    
}
