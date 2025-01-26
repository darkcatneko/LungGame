using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using UnityEngine;

public class BossTriggerGameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] private AudioData breathingHard;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverUI.SetActive(true);
        }
    }

    public void TriggerGameOver()
    {
        AudioManager.Instance.PlayRandomSFX(breathingHard);
        gameOverUI.SetActive(true);
    }
}
