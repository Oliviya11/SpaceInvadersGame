using UnityEngine;

public class UpMover : MonoBehaviour
{
    private const float TIME_AFTER_REMOVE = 2f;
    private float currentTime = 0;
        
    void Update()
    {
        if (currentTime < TIME_AFTER_REMOVE)
        {
            transform.Translate(8 * Vector3.up * Time.deltaTime, Space.Self);
            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}