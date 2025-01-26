using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject player_;
    private void Awake()
    {
        GameManager.Instance.PlayerOBJ = player_;
    }
    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnBackToTitle, cmd => { SceneManager.LoadScene(sceneName); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
