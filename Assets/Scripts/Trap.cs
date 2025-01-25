using UnityEngine;

public class Trap : MonoBehaviour
{
    public Trap showTrap()
    {
        //transform.gameObject.BroadcastMessage("OnShowChunk", SendMessageOptions.DontRequireReceiver);
        //是否需要告知場景生成
        gameObject.SetActive(true);
        return this;
    }

    public Trap hideTrap()
    {
        gameObject.SetActive(false);
        return this;
    }
}
