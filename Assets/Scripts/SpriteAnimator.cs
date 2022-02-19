using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> animationSprites;
    [SerializeField] float animationDeltaTime = 1;
    int animationCurrentSpriteIndex;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite(0);
    }
    
    void Start()
    {
        InvokeRepeating(nameof(ChangeSprite), animationDeltaTime, animationDeltaTime);
    }

    void ChangeSprite()
    {
        ++animationCurrentSpriteIndex;
        if (animationCurrentSpriteIndex >= animationSprites.Count)
        {
            animationCurrentSpriteIndex = 0;
        }
        
        SetSprite(animationCurrentSpriteIndex);
    }

    void SetSprite(int index)
    {
        spriteRenderer.sprite = animationSprites[index];
    }
}
