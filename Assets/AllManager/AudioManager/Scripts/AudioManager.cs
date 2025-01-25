using Game.Input;
using Game.SceneManagement;
using Game.UI;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("必須資料")]
        [SerializeField] AudioSource sFXPlayer;
        [SerializeField] AudioSource bGMPlayer;
        [SerializeField] AudioSource uIPlayer;

        [Header("音效數值")]
        [Range(0f, 1f)] public float SFXAudioVolume;
        [Range(0f, 1f)] public float BGMAudioVolume;
        [Range(0f, 1f)] public float UIAudioVolume;

        [Header("各場景Audio配置")]
        [SerializeField] AudioConfig audioConfig; // 配置所有UI的ScriptableObject

        const float MIN_PITCH = 0.9f;
        const float MAX_PITCH = 1.1f;

        protected override void Awake()
        {
            base.Awake();

            // 檢查音量設置是否已存在，只有在不存在時才設置預設值
            SFXAudioVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            BGMAudioVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            UIAudioVolume = PlayerPrefs.GetFloat("UIVolume", 0.5f);
        }

        void OnEnable()
        {
            LoadSceneManager.OnSceneLoaded += LoadSceneAudioConfig;
        }

        private void OnDisable()
        {
            LoadSceneManager.OnSceneLoaded -= LoadSceneAudioConfig;
        }


        /// <summary>
        /// 播放隨機音效，並且加上隨機音調
        /// </summary>
        private void PlayRandomSoundInternal(AudioSource player, AudioData audioData)
        {
            player.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
            PlaySoundInternal(player, audioData);
        }

        /// <summary>
        /// 撥放音效的核心邏輯
        /// </summary>
        private void PlaySoundInternal(AudioSource player, AudioData audioData)
        {
            GameObject audioSourceObj = new GameObject("AudioSource");
            audioSourceObj.transform.SetParent(player.transform);

            AudioSource audioSource = audioSourceObj.AddComponent<AudioSource>();
            audioSource.clip = audioData.audioClip;
            audioSource.volume = player == sFXPlayer ? SFXAudioVolume : (player == uIPlayer ? UIAudioVolume : 1f); // 確保音量設置正確
            audioSource.Play();

            Destroy(audioSourceObj, audioData.audioClip.length);
        }

        #region SFX音效

        /// <summary>
        /// 撥放音效
        /// </summary>
        public void PlaySFX(AudioData audioData)
        {
            PlaySoundInternal(sFXPlayer, audioData);
        }

        /// <summary>
        /// 播放隨機音效（帶音調隨機化）
        /// </summary>
        public void PlayRandomSFX(AudioData audioData)
        {
            PlayRandomSoundInternal(sFXPlayer, audioData);
        }

        /// <summary>
        /// 隨機撥放音效
        /// </summary>
        /// <param name="audioData"></param>
        public void PlayRandomSFX(AudioData[] audioDatas)
        {
            AudioData randomAudioData = audioDatas[Random.Range(0, audioDatas.Length)];
            PlayRandomSoundInternal(sFXPlayer, randomAudioData);
        }

        #endregion

        #region UI音效

        /// <summary>
        /// 撥放UI音效
        /// </summary>
        public void PlayUISound(AudioData audioData)
        {
            PlaySoundInternal(uIPlayer, audioData);
        }

        /// <summary>
        /// 播放隨機UI音效（帶音調隨機化）
        /// </summary>
        public void PlayRandomUISound(AudioData audioData)
        {
            PlayRandomSoundInternal(uIPlayer, audioData);
        }

        /// <summary>
        /// 隨機撥放UI音效
        /// </summary>
        /// <param name="audioData"></param>
        public void PlayRandomUISound(AudioData[] audioDatas)
        {
            AudioData randomAudioData = audioDatas[Random.Range(0, audioDatas.Length)];
            PlayRandomSoundInternal(uIPlayer, randomAudioData);
        }

        #endregion

        #region BGM音效
        /// <summary>
        /// 撥放BGM音效
        /// </summary>
        /// <param name="index"></param>
        public void PlayBGM(AudioData audioData)
        {
            bGMPlayer.clip = audioData.audioClip;
            bGMPlayer.volume = BGMAudioVolume;
            bGMPlayer.Play();
        }

        /// <summary>
        /// 更新BGM音量
        /// </summary>
        public void UpdateBGMVolume()
        {
            if (bGMPlayer.isPlaying)
            {
                bGMPlayer.volume = BGMAudioVolume;
            }
        }

        #endregion

        /// <summary>
        /// 載入場景時載入Audio
        /// </summary>
        private void LoadSceneAudioConfig()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 從 InputConfig 中獲取當前場景的輸入配置
            AudioConfig.SceneAudio sceneAudio = audioConfig.GetAudioDataForScene(currentSceneName);

            if (sceneAudio != null)
            {
                foreach (AudioData audioData in sceneAudio.startBGMData)
                {
                    // 根據你的邏輯設置 ActionsMap
                    Debug.Log($"Play BGM Sound: {audioData}");
                    PlayBGM(audioData);
                }

                foreach (AudioData audioData in sceneAudio.startSFXData)
                {
                    // 根據你的邏輯設置 ActionsMap
                    Debug.Log($"Play SFX Sound: {audioData}");
                    PlaySFX(audioData);
                }
            }
            else
            {
                Debug.LogWarning($"No AudioConfig for scene: {currentSceneName}");
            }
        }
    }

    [System.Serializable]
    public class AudioData
    {
        public AudioClip audioClip;
    }
}
