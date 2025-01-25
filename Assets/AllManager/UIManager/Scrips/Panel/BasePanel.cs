using Game.Input;
using Game.Audio;
using Game.SceneManagement;
using UnityEngine;

namespace Game.UI
{
    public class BasePanel : MonoBehaviour
    {
        protected GameObject uiPanel;

        protected bool isRemove = false;
        protected UIType panelType; // 將名字替換為UIType

        public UIGroup Group { get; set; } // 每个 UI 面板有一个分组

        [Header("當前的UI需不需要設置成進入UI狀態(也就是會調整InputManagers的按鍵輸入)")]
        [SerializeField] InputType isSetInputActive;

        [Header("按鈕的基礎音效")]
        [SerializeField] protected AudioData audio_NormalBtn;

        protected virtual void Awake()
        {
            uiPanel = gameObject;
        }

        protected virtual void Start()
        {
            InputManagers.Instance.SetInputActive(isSetInputActive);
        }

        protected virtual void OnDestroy()
        {

        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void OpenPanel(UIType type) // 修改參數為 UIType
        {
            panelType = type; // 將傳入的 UIType 儲存
            SetActive(true);
        }

        public virtual void ClosePanel()
        {
            isRemove = true;
            SetActive(false);
            Destroy(gameObject);

            if (UIManager.Instance.PanelDict.ContainsKey(panelType)) // 使用 UIType 作為鍵
            {
                UIManager.Instance.PanelDict.Remove(panelType);
            }

            if(UIManager.Instance.IsAllUIClosed(UIGroup.Menu))
            {
                Debug.Log("全清除");
                InputManagers.Instance.SetInputActive(InputType.Gameplay);
            }
        }
    }
}
