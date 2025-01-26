using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.Audio;
using Gamemanager;

public class NeedleFaller : MonoBehaviour
{
    [SerializeField] bool fall = false;
    

    // Update is called once per frame
    void Update()
    {
        if (!fall && Mathf.Abs(transform.position.z - GameManager.Instance.PlayerOBJ.transform.position.z) < 15f) 
        {
            fall = true;
            transform.DOMoveY(-0.7f, 0.15f);
        }
    }
}
