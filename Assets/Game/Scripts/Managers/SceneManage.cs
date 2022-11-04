using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Managers
{
    public sealed class SceneManage : MonoBehaviour
    {
        public enum SceneManageEvents
        {
            RestartScene,
            NextScene,
            MainMenu,
        }

        public static event Action<SceneManageEvents> SceneEvent;

        private void OnEnable()
        {
            SceneEvent += SceneManageEvent;
        }
        
        
        private void OnDisable()
        {
            SceneEvent -= SceneManageEvent;
        }

        private void SceneManageEvent(SceneManageEvents sceneManageEvent)
        {
            switch (sceneManageEvent)
            {
                case SceneManageEvents.RestartScene:
                    RestartScene();
                    break;
                case SceneManageEvents.NextScene:
                    NextScene();
                    break;
                case SceneManageEvents.MainMenu:
                    GoScene(1);
                    break;
            }
        }

        public static void GoScene(int id)
        {
            if (SceneManager.sceneCountInBuildSettings > id)
            {
                SceneManager.LoadScene(id);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        private static void NextScene()
        {
            
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }else
            {
                SceneManager.LoadScene(0);
            }
        }

        private static void RestartScene()
        {
            int sceneCount = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneCount);
        }

        public static void OnSceneEvent(SceneManageEvents sceneManageEvent)
        {
            SceneEvent?.Invoke(sceneManageEvent);
        }

        [SerializeField] private SceneManageEvents editorSceneManageEvent;

        public void EditorEventSend()
        {
            OnSceneEvent(editorSceneManageEvent);
        }

        public int GetSceneId()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
    

    #region Scene Manage

#if UNITY_EDITOR
    [CustomEditor(typeof(SceneManage))]
    public class SceneManageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SceneManage myTarget = (SceneManage) target;

            if (GUILayout.Button("Scene Event"))
            {
                myTarget.EditorEventSend();
            }
        }

    }
#endif

    #endregion
}
