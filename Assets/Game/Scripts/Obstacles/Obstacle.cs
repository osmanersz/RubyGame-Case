using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameObject particleEffectPrefab;
        [SerializeField] private bool canDestroy;
        public UnityEvent onHit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            onHit?.Invoke();
            var collisionPoint = other.ClosestPoint(transform.position);
            CreateParticleEffect(collisionPoint);
            if (canDestroy)
                Destroy(gameObject);
        }

        private void CreateParticleEffect(Vector3 spawnPos)
        {
            var particleEffect = Instantiate(particleEffectPrefab, spawnPos, Quaternion.identity);
                Destroy(particleEffect, 1f);
        }
    }
}
