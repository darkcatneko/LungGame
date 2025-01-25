using Game.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenu : BasePanel
    {
        [Header("設定通用按鈕")]
        [SerializeField] Button Btn_Start;
        [SerializeField] Button Btn_Settings;
        [SerializeField] Button Btn_Exit;

        [Header("轉換的場景名稱")]
        [SerializeField] SceneType sceneType;

        protected override void Awake()
        {
            base.Awake();

            //設定通用按鈕
            InitializeCommonButtons();
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
        /// 初始化通用按鈕
        /// </summary>
        void InitializeCommonButtons()
        {
            Btn_Start.onClick.AddListener(() => OnBtn_Start());
            Btn_Settings.onClick.AddListener(() => OnBtn_Settings());
            Btn_Exit.onClick.AddListener(() => OnBtn_Exit());
        }

        //設定通用按鈕
        void OnBtn_Start()
        {
            Debug.Log("Click OnBtn_Start");
            //AudioManager.Instance.PlayUISound(audio_NormalBtn);
            LoadSceneManager.Instance.LoadScene(sceneType);
        }
        void OnBtn_Settings()
        {
            Debug.Log("Click OnBtn_Settings");
            //AudioManager.Instance.PlayUISound(audio_NormalBtn);
            UIManager.Instance.OpenPanel(UIType.SettingsMenu);
        }
        void OnBtn_Exit()
        {
            Debug.Log("Click OnBtn_Exit");
            //AudioManager.Instance.PlayUISound(audio_NormalBtn);
#if UNITY_EDITOR
            // 在編輯器中停止播放模式
            UnityEditor.EditorApplication.isPlaying = false;
#else
    // 在執行檔中退出應用程式
    Application.Quit();
#endif
        }

    }
}

