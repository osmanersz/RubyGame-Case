using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Ui.LevelCompleteUi
{
    public class LevelCompleteUiManager : MonoBehaviour
    {
        private static LevelCompleteUiManager _instance;

        public static LevelCompleteUiManager Instance => _instance ?? (_instance = new LevelCompleteUiManager());

        private void Awake()
        {
            _instance = this;
        }

        [SerializeField] private GameObject canvas;

        public void CanvasControl(bool stat)
        {
            canvas.SetActive(stat);
        }
        public void NextLevelButton()
        {
            SceneManage.OnSceneEvent(SceneManage.SceneManageEvents.NextScene);
        }
        
        public void RestartLevelButton()
        {
            SceneManage.OnSceneEvent(SceneManage.SceneManageEvents.RestartScene);
        }
    }
}
