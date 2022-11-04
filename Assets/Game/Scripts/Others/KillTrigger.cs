using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class KillTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onKill;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _onKill?.Invoke();
            }
        }
    }
}
