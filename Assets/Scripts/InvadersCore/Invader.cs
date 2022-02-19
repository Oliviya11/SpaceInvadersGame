using System;
using UnityEngine;

namespace InvadersCore
{
    public class Invader : MonoBehaviour
    {
        [SerializeField] int scoreForDestroy;
        [SerializeField] Color color;
        public Action<Invader> onDestroyed;
        public int ScoreForDestroy => scoreForDestroy;
        public Color Color => color;

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
}
