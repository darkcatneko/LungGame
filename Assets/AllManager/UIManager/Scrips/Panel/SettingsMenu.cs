using Game.Audio;
using Game.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsMenu : BasePanel
    {
        [Header("設定通用按鈕")]
        [SerializeField] Button Btn_Application;
        [SerializeField] Button Btn_Cancel;
        [SerializeField] Button Btn_Return_To_MainMenu;

        [Header("設定類別按鈕")]
        [SerializeField] Button Btn_Graphics;
        [SerializeField] Button Btn_Sound;
        [SerializeField] Button Btn_Controls;

        [Header("設定音效介面")]
        [SerializeField] GameObject Obj_Slider_SFX_Volume;
        [SerializeField] GameObject Obj_Slider_BGM_Volume;
        [SerializeField] GameObject Obj_Slider_UI_Volume;

        protected override void Awake()
        {
            base.Awake();

            // 初始化音樂界面數值
            InitializeSoundMenu();

            // 設定通用按鈕
            InitializeCommonButtons();
            // 設定類別按鈕
            InitializeCategoryButtons();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <summary>
        /// 初始化音效介面
        /// </summary>
        void InitializeSoundMenu()
        {
            Slider Slider_SFX_Volume = Obj_Slider_SFX_Volume.GetComponentInChildren<Slider>();
            Slider Slider_BGM_Volume = Obj_Slider_BGM_Volume.GetComponentInChildren<Slider>();
            Slider Slider_UI_Volume = Obj_Slider_UI_Volume.GetComponentInChildren<Slider>();

            // 從 PlayerPrefs 加載音量設置
            Slider_SFX_Volume.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            Slider_BGM_Volume.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            Slider_UI_Volume.value = PlayerPrefs.GetFloat("UIVolume", 1f);

            //更新音效數值Text
            OnSFXVolumeChanged(Slider_SFX_Volume.value);
            OnBGMVolumeChanged(Slider_BGM_Volume.value);
            OnUIVolumeChanged(Slider_UI_Volume.value);

            // 添加滑輪變化監聽器
            Slider_SFX_Volume.onValueChanged.AddListener(OnSFXVolumeChanged);
            Slider_BGM_Volume.onValueChanged.AddListener(OnBGMVolumeChanged);
            Slider_UI_Volume.onValueChanged.AddListener(OnUIVolumeChanged);
        }

        /// <summary>
        /// 初始化通用按鈕
        /// </summary>
        void InitializeCommonButtons()
        {
            Btn_Application.onClick.AddListener(() => OnBtn_Application());
            Btn_Cancel.onClick.AddListener(() => OnBtn_Cancel());
            Btn_Return_To_MainMenu.onClick.AddListener(() => OnBtn_Return_To_MainMenu());
        }

        /// <summary>
        /// 初始化類別按鈕
        /// </summary>
        void InitializeCategoryButtons()
        {
            Btn_Graphics.onClick.AddListener(() => OnBtn_Graphics());
            Btn_Sound.onClick.AddListener(() => OnBtn_Sound());
            Btn_Controls.onClick.AddListener(() => OnBtn_Controls());
        }

        //設定通用按鈕

        void OnBtn_Application()
        {
            Debug.Log("Click Btn_Application");
            AudioManager.Instance.PlayUISound(audio_NormalBtn);
        }

        void OnBtn_Cancel()
        {
            Debug.Log("Click Btn_Cancel");
            AudioManager.Instance.PlayUISound(audio_NormalBtn);
            ClosePanel();
        }

        void OnBtn_Return_To_MainMenu()
        {
            Debug.Log("Click Btn_Return_To_MainMenu");
            AudioManager.Instance.PlayUISound(audio_NormalBtn);
            LoadSceneManager.Instance.LoadScene(SceneType.MainMenuScene);
            //GameManager.instance.SwitchScene("MainMenuScene");
        }

        //設定類別按鈕

        void OnBtn_Graphics()
        {
            Debug.Log("Click Btn_Graphics");
        }

        void OnBtn_Sound()
        {
            Debug.Log("Click Btn_Sound");
        }

        void OnBtn_Controls()
        {
            Debug.Log("Click Btn_Controls");
        }

        //設定音效界面

        void OnSFXVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            AudioManager.Instance.SFXAudioVolume = value;

            //設置音效數值Text
            TMP_Text Volume = Obj_Slider_SFX_Volume.transform.Find("Text_Volume").GetComponent<TMP_Text>();
            // 將 value 轉換為百分比格式
            int percentage = Mathf.RoundToInt(value * 100);
            Volume.text = percentage.ToString() + "%";
        }

        void OnBGMVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("BGMVolume", value);
            AudioManager.Instance.BGMAudioVolume = value;
            AudioManager.Instance.UpdateBGMVolume();

            //設置音效數值Text
            TMP_Text Volume = Obj_Slider_BGM_Volume.transform.Find("Text_Volume").GetComponent<TMP_Text>();
            // 將 value 轉換為百分比格式
            int percentage = Mathf.RoundToInt(value * 100);
            Volume.text = percentage.ToString() + "%";
        }

        void OnUIVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("UIVolume", value);
            AudioManager.Instance.UIAudioVolume = value;

            //設置音效數值Text
            TMP_Text Volume = Obj_Slider_UI_Volume.transform.Find("Text_Volume").GetComponent<TMP_Text>();
            // 將 value 轉換為百分比格式
            int percentage = Mathf.RoundToInt(value * 100);
            Volume.text = percentage.ToString() + "%";
        }
    }
}
