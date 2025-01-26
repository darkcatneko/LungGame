using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            winMenu.SetActive(true);
        }
    }
}
