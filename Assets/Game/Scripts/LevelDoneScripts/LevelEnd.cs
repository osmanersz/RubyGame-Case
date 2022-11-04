using Game.Scripts.Player;
using Game.Scripts.Ui.LevelCompleteUi;
using UnityEngine;

namespace Game.Scripts.LevelDoneScripts
{
  public class LevelEnd : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
      {
        PlayerIn(other.gameObject);
      }
    }

    private void PlayerIn(GameObject player)
    {
      player.transform.parent.GetComponent<PlayerInputManager>().GameEnd();
      LevelCompleteUiManager.Instance.CanvasControl(true);
    }
  }
}
