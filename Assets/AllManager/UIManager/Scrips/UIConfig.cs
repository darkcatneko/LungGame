using System.Collections.Generic;
using System;
using UnityEngine;
using Game.SceneManagement;

namespace Game.UI
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "UI/UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [Serializable]
        public class SceneUI
        {
            public SceneType sceneType;
            public List<UIType> startPanels;
        }

        [Serializable]
        public class UIPanel
        {
            public UIType uiType;
            public string resourcePath; // Resources�������|
        }

        public List<UIPanel> uiPanels;
        public List<SceneUI> sceneUIConfigs;

        private Dictionary<UIType, string> _pathDict;

        public List<UIType> GetUIPanelForScene(string sceneName)
        {
            //Debug.Log(sceneUIConfigs.Count);
            foreach (var sceneUI in sceneUIConfigs)
            {
                //Debug.Log("�ؼ�" + sceneName);
                //Debug.Log("��쪺" + sceneUI.sceneName);
                if (sceneUI.sceneType.ToString() == sceneName)
                {
                    return sceneUI.startPanels;
                }
            }
            return null; // �p�G���������������t�m�A��^��
        }

        private void OnEnable()
        {
            _pathDict = new Dictionary<UIType, string>();
            foreach (UIPanel panel in uiPanels)
            {
                _pathDict[panel.uiType] = panel.resourcePath;
            }
        }

        public string GetPath(UIType uiType)
        {
            return _pathDict.TryGetValue(uiType, out string path) ? path : null;
        }
    }
}
