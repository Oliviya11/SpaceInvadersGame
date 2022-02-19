using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Action<Invader> onDestroyed;
    
    private void OnCollision()
    {
        onDestroyed.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnCollision();
    }
}
