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
        /// �I�s���UI
        /// </summary>
        private void ShowTransitionUI()
        {
            Debug.Log("���0000000");
            UIManager.Instance.OpenPanel(UIType.SceneLoading_TransitionPanel);
        }

        /// <summary>
        /// �������UI�P�Ҧ�UI
        /// </summary>
        private void HideTransitionUI()
        {
            Debug.Log("�������0000000");
            UIManager.Instance.CloseAllPanels();
        }
    }
}
