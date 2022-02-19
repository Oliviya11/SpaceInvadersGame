using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float shootingDeltaTime = 2;
    
    void Start()
    {
        InvokeRepeating(nameof(Shoot), shootingDeltaTime, shootingDeltaTime);
    }
    
    private void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
