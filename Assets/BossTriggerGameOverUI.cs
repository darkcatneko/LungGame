using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerGameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverUI.SetActive(true);
        }
    }

    public void TriggerGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
