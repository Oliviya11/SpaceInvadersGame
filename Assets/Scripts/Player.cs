using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utils;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Projectile projectilePrefab;
    Vector2 size;
    bool isProjectile;
    public Action onDestroyed;
    private Camera myCamera;


    void Awake()
    {
        myCamera = Camera.main;
        InputManager.OnSpacePressed += Shoot;
        InputManager.OnLeftArrowPressed += OnLeftArrowPressed;
        InputManager.OnRightArrowPressed += OnRightArrowPressed;
    }
    void Start()
    {
        size = GetComponent<SpriteRenderer>().size;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnCollision();
    }

    private void OnDestroy()
    {
        InputManager.OnSpacePressed -= Shoot;
        InputManager.OnLeftArrowPressed -= OnLeftArrowPressed;
        InputManager.OnRightArrowPressed -= OnRightArrowPressed;
    }
    
    void OnRightArrowPressed()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        ClampPos(ref pos.x);
        transform.position = pos;
    }

    void OnLeftArrowPressed()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        ClampPos(ref pos.x);
        transform.position = pos;
    }

    float GetRightEdge()
    {
        Vector3 rightEdge = myCamera.ViewportToWorldPoint(Vector3.right);
        float delta = 0.5f * size.x;
        rightEdge.x -= delta;
        return rightEdge.x;
    }

    float GetLeftEdge()
    {
        Vector3 leftEdge = myCamera.ViewportToWorldPoint(Vector3.zero);
        float delta = 0.5f * size.x;
        leftEdge.x += delta;
        return leftEdge.x;
    }

    void ClampPos(ref float x)
    {
        x = Mathf.Clamp(x, GetLeftEdge(), GetRightEdge());
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
        onDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
