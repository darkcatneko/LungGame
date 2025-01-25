using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;

namespace Game.SceneManagement
{
    public enum SceneType
    {
        MainMenuScene,
        GameScene1,
        GameScene2,
        LoadingScene,
    }

    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        public static event Action OnSceneTransitionStart;

        public static event Action OnSceneLoaded;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnSceneLoaded?.Invoke();
        }

        /// <summary>
        /// 載入場景
        /// </summary>
        /// <param name="sceneType">目標場景類型</param>
        public void LoadScene(SceneType sceneType)
        {
            StartCoroutine(IELoadScene(sceneType));
        }

        IEnumerator IELoadScene(SceneType sceneType)
        {
            OnSceneTransitionStart?.Invoke();

            yield return new WaitForSeconds(0.5f);

            // 枚舉轉換為字符串以加載場景
            LoadingScene.LoadScene(sceneType.ToString());
        }
    }
}
