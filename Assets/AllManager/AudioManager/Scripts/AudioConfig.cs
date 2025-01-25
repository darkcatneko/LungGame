using Game.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Audio/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [Serializable]
        public class SceneAudio
        {
            public SceneType sceneType; // �����W��
            public List<AudioData> startBGMData;
            public List<AudioData> startSFXData; // �ӳ�������l��J�ʧ@
        }

        public List<SceneAudio> sceneAudios; // �Ҧ���������J�t�m

        /// <summary>
        /// �ھڷ�e�����W�������������J�t�m
        /// </summary>
        /// <param name="sceneName">�����W��</param>
        /// <returns>�ӳ�������J�ʧ@�C��</returns>
        public SceneAudio GetAudioDataForScene(string sceneName)
        {
            foreach (var sceneAudio in sceneAudios)
            {
                if (sceneAudio.sceneType.ToString() == sceneName)
                {
                    return sceneAudio;
                }
            }
            return null; // �p�G���������������t�m�A��^��
        }
    }

}
