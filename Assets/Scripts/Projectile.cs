using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 20f;
        [SerializeField] Vector3 direction = Vector3.up;
        public Action<Projectile> onDestroyed;
        private new BoxCollider2D collider;

        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
        }

        private void OnDestroy()
        {
            onDestroyed?.Invoke(this);
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollision()
        {
            Destroy(gameObject);
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