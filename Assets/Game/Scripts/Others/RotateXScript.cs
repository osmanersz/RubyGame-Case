using UnityEngine;

namespace Game.Scripts
{
    public class RotateXScript : MonoBehaviour
    {
        [SerializeField] private Transform mesh;
        [SerializeField] private float rotateSpeed = 1f;
        void Update()
        {
            mesh.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0)); 
        }
    }
}
