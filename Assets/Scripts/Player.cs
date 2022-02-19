using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Projectile projectilePrefab;
    Vector2 size;
    bool isProjectile;

    void Start()
    {
        size = GetComponent<SpriteRenderer>().size;
    }
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }
        
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        float delta = 0.5f * size.x;
        rightEdge.x -= delta;
        leftEdge.x += delta;
        
        pos.x = Mathf.Clamp(pos.x, leftEdge.x, rightEdge.x);
        transform.position = pos;
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        if (!isProjectile)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.onDestroyed += OnProjectileDestroyed;
            isProjectile = true;
        }
    }

    private void OnProjectileDestroyed(Projectile projectile)
    {
        isProjectile = false;
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
