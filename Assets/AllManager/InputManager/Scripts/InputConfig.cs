using Game.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Input
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Input/InputConfig")]
    public class InputConfig : ScriptableObject
    {
        [Serializable]
        public class SceneInput
        {
            public SceneType sceneType; // �����W��
            public List<InputType> startInputActions; // �ӳ�������l��J�ʧ@
            public bool uiOnly;
        }

        public List<SceneInput> sceneInputs; // �Ҧ���������J�t�m

        /// <summary>
        /// �ھڷ�e�����W�������������J�t�m
        /// </summary>
        /// <param name="sceneName">�����W��</param>
        /// <returns>�ӳ�������J�ʧ@�C��</returns>
        public SceneInput GetInputActionsForScene(string sceneName)
        {
            foreach (var sceneInput in sceneInputs)
            {
                if (sceneInput.sceneType.ToString() == sceneName)
                {
                    return sceneInput;
                }
            }
            return null; // �p�G���������������t�m�A��^��
        }
    }

}
