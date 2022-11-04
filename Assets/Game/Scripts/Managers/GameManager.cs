using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {

        private void Awake()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            QualitySettings.antiAliasing = 0;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            DOTween.Init();
        }
    }
}

