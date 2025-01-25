using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.SceneManagement;
using Game.Input;

namespace Game.UI
{
    /// <summary>
    /// UI類型
    /// </summary>
    public enum UIType
    {
        //Gameplay 遊戲類 UI
        HUDMenu,
        //Menu 菜單類 UI
        MainMenu,
        SettingsMenu,

        BagMenu,
        DialoguePanel,

        GameStart_TransitionPanel,
        SceneLoading_TransitionPanel,
        GameOverMenu,
        GameEndingMenu,
        //Popup 彈窗類 UI
        GetItemPopup,
        GameBlood_TransitionPanel,
    }

    /// <summary>
    /// UI集合
    /// </summary>
    public enum UIGroup
    {
        Gameplay,  // 遊戲類 UI，比如 HUD、遊戲中顯示
        Menu,      // 菜單類 UI，比如設置界面、背包等
        Popup      // 彈窗類 UI，比如提示框
    }

    public class UIManager : Singleton<UIManager>
    {
        private Transform _uiRoot;
        public Transform UIRoot => _uiRoot;
        private Dictionary<UIType, GameObject> _prefabDict; // 快取已加載的預置件
        public Dictionary<UIType, BasePanel> PanelDict { get; private set; }

        [Header("各場景UI配置")]
        [SerializeField] UIConfig uiConfig; // 配置所有UI的ScriptableObject

        protected override void Awake()
        {
            base.Awake();
            _prefabDict = new Dictionary<UIType, GameObject>();
            PanelDict = new Dictionary<UIType, BasePanel>();
            EnsureUIRootExists();
        }

        private void OnEnable()
        {
            LoadSceneManager.OnSceneLoaded += CloseAllPanels;
            LoadSceneManager.OnSceneLoaded += LoadSceneUIConfig;

            //InputManagers.OpenSettingsEvent += OnOpenSettings;
            InputManagers.CloseUIEvent += OnCloseUI;
        }

        private void OnDisable()
        {
            LoadSceneManager.OnSceneLoaded -= CloseAllPanels;
            LoadSceneManager.OnSceneLoaded -= LoadSceneUIConfig;

            //InputManagers.OpenSettingsEvent -= OnOpenSettings;
            InputManagers.CloseUIEvent -= OnCloseUI;
        }

        private void OnOpenSettings()
        {
            OpenPanel(UIType.SettingsMenu);
        }

        private void OnCloseUI()
        {
            CloseAllPanels(UIGroup.Menu);
        }

        /// <summary>
        /// 確保UIRoot存在
        /// </summary>
        private void EnsureUIRootExists()
        {
            if (_uiRoot == null)
            {
                GameObject canvasObject = GameObject.Find("Canvas") ?? new GameObject("Canvas");
                _uiRoot = canvasObject.transform;
            }
        }

        /// <summary>
        /// 開啟UI介面
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public BasePanel OpenPanel(UIType uiType)
        {
            if (PanelDict.ContainsKey(uiType))
            {
                Debug.LogWarning($"界面已打开: {uiType}");
                return null;
            }

            GameObject panelPrefab = LoadPanelPrefab(uiType);
            if (panelPrefab == null)
            {
                Debug.LogError($"无法加载预置件: {uiType}");
                return null;
            }

            GameObject panelObject = Instantiate(panelPrefab, _uiRoot, false);
            BasePanel panel = panelObject.GetComponent<BasePanel>();
            panel.Group = GetUIGroup(uiType); // 根据 UIType 设置分组
            PanelDict[uiType] = panel;
            panel.OpenPanel(uiType);
            return panel;
        }

        /// <summary>
        /// 載入UI預置件
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private GameObject LoadPanelPrefab(UIType uiType)
        {
            if (_prefabDict.TryGetValue(uiType, out GameObject cachedPrefab))
            {
                return cachedPrefab;
            }

            string path = uiConfig.GetPath(uiType);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError($"UI類型 {uiType} 未配置路徑");
                return null;
            }

            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab != null)
            {
                _prefabDict[uiType] = prefab;
            }
            return prefab;
        }

        /// <summary>
        /// 關閉該UI
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public bool ClosePanel(UIType uiType)
        {
            if (!PanelDict.TryGetValue(uiType, out BasePanel panel))
            {
                Debug.LogWarning($"界面未打開: {uiType}");
                return false;
            }

            panel.ClosePanel();
            PanelDict.Remove(uiType);
            return true;
        }

        /// <summary>
        /// 清除全部UI
        /// </summary>
        public void CloseAllPanels()
        {
            var panelsToClose = new List<BasePanel>(PanelDict.Values); // 複製當前的值
            foreach (var panel in panelsToClose)
            {
                if (panel != null)
                {
                    panel.ClosePanel();
                }
            }

            PanelDict.Clear(); // 最後再清空字典
        }

        /// <summary>
        /// 清理該類型的全部UI
        /// </summary>
        /// <param name="group"></param>
        public void CloseAllPanels(UIGroup group)
        {
            var panelsToClose = new List<UIType>();

            // 找到所有屬於該 UIGroup 的面板
            foreach (var kvp in PanelDict)
            {
                if (kvp.Value.Group == group)
                {
                    panelsToClose.Add(kvp.Key);
                }
            }

            // 關閉這些面板
            foreach (var uiType in panelsToClose)
            {
                ClosePanel(uiType);
            }
        }

        /// <summary>
        /// 載入場景時載入UI
        /// </summary>
        private void LoadSceneUIConfig()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            List<UIType> uiTypes = uiConfig.GetUIPanelForScene(currentSceneName);

            if (uiTypes != null)
            {
                //Debug.Log(uiTypes);
                foreach (UIType uiType in uiTypes)
                {
                    OpenPanel(uiType);
                }
            }
        }


        /// <summary>
        /// 獲取該UI類型的集團
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private UIGroup GetUIGroup(UIType uiType)
        {
            switch (uiType)
            {
                case UIType.HUDMenu:
                    return UIGroup.Gameplay;

                case UIType.MainMenu:
                case UIType.SettingsMenu:
                case UIType.BagMenu:
                case UIType.DialoguePanel:
                case UIType.GameStart_TransitionPanel:
                case UIType.SceneLoading_TransitionPanel:
                case UIType.GameOverMenu:
                case UIType.GameEndingMenu:
                    return UIGroup.Menu;
                
                case UIType.GameBlood_TransitionPanel:
                case UIType.GetItemPopup:
                    return UIGroup.Popup;
                default:
                    return UIGroup.Gameplay;
            }
        }

        /// <summary>
        /// 判定當前UI的集團是否全部關閉了
        /// </summary>
        /// <returns></returns>
        public bool IsAllUIClosed(UIGroup uIGroup)
        {
            foreach (var panel in PanelDict.Values)
            {
                if (panel.Group == uIGroup)
                {
                    return false; // 如果還有 Menu 類型的面板未關閉
                }
            }
            return true; // 所有 Menu 類型的面板都已關閉
        }
    }
}
