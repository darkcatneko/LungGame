using Game.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.Input
{
    public enum InputType
    {
        Gameplay,
        UI,
    }

    public class InputManagers : Singleton<InputManagers>, GameInputActions.IGameplayActions, GameInputActions.IUIActions
    {
        public static event Action OpenSettingsEvent;

        public static event Action CloseUIEvent;

        GameInputActions inputActions;

        private bool uiOnly;

        [Header("各場景input配置")]
        [SerializeField] InputConfig inputConfig; // 配置所有UI的ScriptableObject

        protected override void Awake()
        {
            base.Awake();

            if (inputActions == null)
            {
                inputActions = new GameInputActions();

                inputActions.Gameplay.SetCallbacks(this);
                inputActions.UI.SetCallbacks(this);
            }
        }

        void OnEnable()
        {
            LoadSceneManager.OnSceneLoaded += LoadSceneActionsMapConfig;
        }

        private void OnDisable()
        {
            LoadSceneManager.OnSceneLoaded -= LoadSceneActionsMapConfig;
        }

        /// <summary>
        /// 載入場景時調整ActionsMap
        /// </summary>
        private void LoadSceneActionsMapConfig()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 從 InputConfig 中獲取當前場景的輸入配置
            InputConfig.SceneInput sceneInput = inputConfig.GetInputActionsForScene(currentSceneName);

            if (sceneInput != null)
            {
                foreach (InputType inputAction in sceneInput.startInputActions)
                {
                    // 根據你的邏輯設置 ActionsMap
                    Debug.Log($"Setting input action: {inputAction}");
                    SetInputActive(inputAction);
                    //SetActionsMap(inputAction);
                }
                uiOnly = sceneInput.uiOnly;
            }
            else
            {
                Debug.LogWarning($"No input actions found for scene: {currentSceneName}");
            }
        }

        #region Gameplay

        public void OnAxes(InputAction.CallbackContext context)
        {

        }

        public void OnJump(InputAction.CallbackContext context)
        {

        }

        public void OnAttack(InputAction.CallbackContext context)
        {

        }

        public void OnChargedAttack(InputAction.CallbackContext context)
        {

        }

        public void OnRun(InputAction.CallbackContext context)
        {

        }

        public void OnRoll(InputAction.CallbackContext context)
        {

        }

        public void OnDash(InputAction.CallbackContext context)
        {

        }

        public void OnCameraRotation_Ctrl(InputAction.CallbackContext context)
        {

        }

        public void OnCameraRotation_Alt(InputAction.CallbackContext context)
        {

        }

        public void OnLockEnemy(InputAction.CallbackContext context)
        {

        }

        public void OnOpenSettings(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("打開遊戲設定");
                OpenSettingsEvent?.Invoke();
            }
        }

        #endregion

        #region UI

        public void OnDialogue(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("啟動對話功能");
            }
        }

        public void OnBackPack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("打開背包");
            }
        }

        public void OnGameMenu(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("打開遊戲菜單");
            }
        }

        public void OnCloseUI(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("關閉遊戲UI");
                CloseUIEvent?.Invoke();
            }
        }

        #endregion

        #region 設置按鍵輸入行為

        /// <summary>
        /// 設置按鍵輸入地圖
        /// </summary>
        /// <param name="inputType"></param>
        private void SetActionsMap(InputType inputType)
        {
            Debug.Log("遊戲輸入切換至" + inputType.ToString());
            //Debug.Log(inputActions);
            foreach (InputActionMap inputAction in inputActions.asset.actionMaps)
            {
                if (inputAction.name == inputType.ToString())
                {
                    Debug.Log(inputAction.name + "輸入系統啟動");
                    inputAction.Enable();
                }
                else
                {
                    inputAction.Disable();
                }
            }
        }

        /// <summary>
        /// 進入UI界面
        /// </summary>
        private void EnterUI()
        {
            SetActionsMap(InputType.UI);

            // 顯示滑鼠
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 停止遊戲時間（如有需要）
            Time.timeScale = 1;
        }

        /// <summary>
        /// 退出UI界面
        /// </summary>
        private void EnterGamePlay()
        {
            SetActionsMap(InputType.Gameplay);

            // 隱藏滑鼠並將其位置設為螢幕中央
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // 恢復遊戲時間
            Time.timeScale = 1;
        }

        /// <summary>
        /// 設置按鍵輸入狀態
        /// </summary>
        /// <param name="target">目標按鍵輸入狀態</param>
        public void SetInputActive(InputType inputType)
        {
            if (!uiOnly)
            {
                switch (inputType)
                {
                    case InputType.Gameplay:
                        {
                            EnterGamePlay();
                            break;
                        }
                    case InputType.UI:
                        {
                            EnterUI();
                            break;
                        }
                }
            }
        }
    }

    #endregion
}
