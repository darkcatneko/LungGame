using UnityEngine;

namespace Game.UI
{
    public class TransitionUIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Game.SceneManagement.LoadSceneManager.OnSceneTransitionStart += ShowTransitionUI;
            Game.SceneManagement.LoadingScene.OnSceneTransitionEnd += HideTransitionUI;
        }

        private void OnDisable()
        {
            Game.SceneManagement.LoadSceneManager.OnSceneTransitionStart -= ShowTransitionUI;
            Game.SceneManagement.LoadingScene.OnSceneTransitionEnd -= HideTransitionUI;
        }

        /// <summary>
        /// 呼叫轉場UI
        /// </summary>
        private void ShowTransitionUI()
        {
            Debug.Log("轉場0000000");
            UIManager.Instance.OpenPanel(UIType.SceneLoading_TransitionPanel);
        }

        /// <summary>
        /// 關閉轉場UI與所有UI
        /// </summary>
        private void HideTransitionUI()
        {
            Debug.Log("結束轉場0000000");
            UIManager.Instance.CloseAllPanels();
        }
    }
}
