using Game.Scripts.Interface;
using Game.Scripts.Ui.ScoreUi;
using UnityEngine;

namespace Game.Scripts.Collectables
{
    public abstract class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private ScoreTypes _scoreType;
        public void Collect()
        {
            GiveReward();
        }

        protected virtual void GiveReward()
        {
            Destroy(gameObject);
            ScoreUiManager.OnCollectableCollected(_scoreType);
        }
    }
}
