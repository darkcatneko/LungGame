using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;

namespace Game.SceneManagement
{
    public class LoadingScene : MonoBehaviour
    {
        public static event Action OnSceneTransitionEnd;

        public static string nextScene;

        private void Start()
        {
            Debug.Log(nextScene);
            //Game.UI.UIManager.Instance.CloseAllPanels();
            // �}�l�[���U�@�ӳ���
            StartCoroutine(LoadNextScene());
        }

        // �}�l����å[������
        public static void LoadScene(string sceneName)
        {
            Debug.Log("�n���J������" + sceneName);

            // �]�m�U�@�ӭn�[���������W��
            nextScene = sceneName;

            Debug.Log("��ڸ��J������" + nextScene);

            SceneManager.LoadScene("LoadingScene");
        }

        private IEnumerator LoadNextScene()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(0.5f);
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }

            OnSceneTransitionEnd?.Invoke();
        }
    }
}
