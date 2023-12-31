using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetSpriteRenderer;
    [SerializeField] private Sprite[] walkSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite fallSprite;

    [SerializeField] private float initialAnimationSpeed;

    private float animationSpeedFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        animationSpeedFactor = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetAnimationSpeedFactor(float f)
    {
        animationSpeedFactor = f;
    }

    public void PlayWalkAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(Walk());
    }

    public void PlayJumpAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(Jump());
    }

    public void SetFallSprite()
    {
        StopAllCoroutines();
        targetSpriteRenderer.sprite = fallSprite;
    }

    private IEnumerator Walk()
    {
        int sprite = 0;
        while (sprite < walkSprites.Length)
        {
            targetSpriteRenderer.sprite = walkSprites[sprite];
            sprite = (sprite + 1) % walkSprites.Length;
            yield return new WaitForSeconds(1f / (initialAnimationSpeed * animationSpeedFactor));
        }
    }

    private IEnumerator Jump()
    {
        int sprite = 0;
        while (sprite < jumpSprites.Length)
        {
            targetSpriteRenderer.sprite = jumpSprites[sprite];
            sprite++;
            yield return new WaitForSeconds(1f / (initialAnimationSpeed * animationSpeedFactor));
        }
    }
}
