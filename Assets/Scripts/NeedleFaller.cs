using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.Audio;
using Gamemanager;

public class NeedleFaller : MonoBehaviour
{
    [SerializeField] bool fall = false;
    [SerializeField] private AudioData ot_Needle;

    // Update is called once per frame
    void Update()
    {
        if (!fall && Mathf.Abs(transform.position.z - GameManager.Instance.PlayerOBJ.transform.position.z) < 15f) 
        {
            fall = true;
            AudioManager.Instance.PlayRandomSFX(ot_Needle);
            transform.DOMoveY(-0.7f, 0.15f);
        }
    }
}
