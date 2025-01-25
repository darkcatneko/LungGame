using UnityEngine;
using DG.Tweening;

public class MonsteBreathrBehavior : MonoBehaviour
{
    [SerializeField] Transform thisTransform_;
    [SerializeField] bool breath_ = false;
    float elapsedTime = 0f;
    [SerializeField] GameObject thin_;
    [SerializeField] GameObject fat_;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

}
