using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Camera_Scripts
{
    public class CameraShakeController : MonoBehaviour
    {
        private static CameraShakeController _instance;
        public static CameraShakeController Instance { get { return _instance ?? (_instance = new CameraShakeController()); } }

        private Vector3 _orginalPos = new Vector3(0, 0, 0);

        private void Awake()
        {
            _instance = this;
            _orginalPos = gameObject.transform.localPosition;
        }

        public void Shake(float duration, float amount)
        {
            //  _orginalPos = gameObject.transform.localPosition;
            StopAllCoroutines();
            StartCoroutine(CShake(duration, amount));
        }


        private IEnumerator CShake(float duration, float amount)
        {
            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                transform.localPosition = new Vector3
                (
                    _orginalPos.x + Random.insideUnitSphere.x * amount
                    ,
                    _orginalPos.y + Random.insideUnitSphere.y * amount
                    ,
                    _orginalPos.z
                );
                duration -= Time.deltaTime;
                yield return null;
            }
            transform.localPosition = _orginalPos;
        }

        public void HitShake()
        {
            Shake(0.05f,0.25f);
        } 
        public void GatePassShake()
        {
            Shake(0.05f,0.035f);
        }
    }
}
