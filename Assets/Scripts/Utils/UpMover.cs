using UnityEngine;

namespace Utils
{
    public class UpMover : MonoBehaviour
    { 
        [SerializeField] float timeAfterDestroyed = 2f;
        [SerializeField] Vector3 translation;
        private float currentTime = 0;
        
        void Update()
        {
            if (currentTime < timeAfterDestroyed)
            {
                transform.Translate(translation * Time.deltaTime, Space.Self);
                currentTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }   
}