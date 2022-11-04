using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Collectables.Star
{
    public class Star : Collectable
    {
        [SerializeField] private GameObject particleEffectPrefab;
        [SerializeField] private Transform mesh;
        [SerializeField] private float rotateSpeed = 1f;
        private bool _isCollected;

        private void Start()
        {
            var meshLocalPos = mesh.localPosition;
            meshLocalPos.y += 0.15f;
            mesh.localPosition = meshLocalPos;
            mesh.DOLocalMoveY(0, 1.25f).SetLoops(-1, LoopType.Yoyo);
        }

        private void Update()
        {
            mesh.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        protected override void GiveReward()
        {
            if (_isCollected)
                return;
            _isCollected = true;
            var particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            mesh.DOShakeScale(0.1f,0.5f,5,1).OnComplete(AnimationEnd);
            Destroy(particleEffect,1f);
        }

        private void AnimationEnd()
        {
            mesh.localScale=Vector3.one;
            base.GiveReward();
        }
    }
}
