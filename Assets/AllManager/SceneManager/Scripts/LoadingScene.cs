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
            // 開始加載下一個場景
            StartCoroutine(LoadNextScene());
        }

        // 開始轉場並加載場景
        public static void LoadScene(string sceneName)
        {
            Debug.Log("要載入的場景" + sceneName);

            // 設置下一個要加載的場景名稱
            nextScene = sceneName;

            Debug.Log("實際載入的場景" + nextScene);

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
