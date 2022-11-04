using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.GameEvent
{
    public class GameEventTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _onEvent?.Invoke();
            }
        }
    }
}
