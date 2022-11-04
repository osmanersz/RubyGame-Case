using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Ui.GameOverUi
{
    public class GameOverUiManager : MonoBehaviour
    {
        private static GameOverUiManager _instance;

        public static GameOverUiManager Instance => _instance ?? (_instance = new GameOverUiManager());

        private void Awake()
        {
            _instance = this;
        }
        [SerializeField] private GameObject canvas;

        public void CanvasControl(bool stat)
        {
            canvas.SetActive(stat);
        }
        
        public void RestartLevelButton()
        {
            SceneManage.OnSceneEvent(SceneManage.SceneManageEvents.RestartScene);
        }
    }
}
