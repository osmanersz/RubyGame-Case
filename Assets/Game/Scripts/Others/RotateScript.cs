using UnityEngine;

namespace Game.Scripts
{
    public class RotateScript : MonoBehaviour
    {
        [SerializeField] private Transform mesh;
        [SerializeField] private float rotateSpeed = 1f;
        void Update()
        {
            mesh.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0)); 
        }
    }
}
