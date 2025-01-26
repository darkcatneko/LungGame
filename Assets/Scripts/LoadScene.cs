using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClickStartGame()
    {
        Debug.Log("Button clicked!");
        // 示例操作：切换场景
        SceneManager.LoadScene("GamePlay");
    }

    public void OnButtonClickEndGame()
    {
        Debug.Log("Button clicked!");
        Application.Quit();
    }
}