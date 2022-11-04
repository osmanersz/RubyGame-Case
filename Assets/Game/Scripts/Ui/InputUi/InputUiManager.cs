using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Ui.InputUi
{
    public class InputUiManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private UnityEvent _onGameStart;

        public void TapToPlayButton()
        {
            _onGameStart?.Invoke();
            CanvasControl(false);
        }

        private void CanvasControl(bool stat)
        {
            canvas.SetActive(stat);
        }
    }
}
