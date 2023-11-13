using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteMask maskYin;
    [SerializeField] private SpriteMask maskYang;

    [SerializeField] private Sprite[] walkSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] fallSprites;

    [SerializeField] private float initialAnimationSpeed;

    private float animationSpeedFactor;
    
    private bool yin;
    
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

    public void PlayWalkAnimation(bool y)
    {
        yin = y;
        StopAllCoroutines();
        StartCoroutine(Walk());
    }

    public void PlayJumpAnimation(bool y)
    {
        yin = y;
        StopAllCoroutines();
        StartCoroutine(Jump());
    }

    public void PlayFallAnimation(bool y)
    {
        yin = y;
        StopAllCoroutines();
        StartCoroutine(Fall());
    }

    private IEnumerator Walk()
    {
        int sprite = 0;
        while (sprite < walkSprites.Length)
        {
            (yin ? maskYin : maskYang).sprite = walkSprites[sprite];
            sprite = (sprite + 1) % walkSprites.Length;
            yield return new WaitForSeconds(1f / (initialAnimationSpeed * animationSpeedFactor));
        }
    }

    private IEnumerator Jump()
    {
        int sprite = 0;
        while (sprite < jumpSprites.Length)
        {
            (yin ? maskYin : maskYang).sprite = jumpSprites[sprite];
            sprite++;
            yield return new WaitForSeconds(1f / (initialAnimationSpeed * animationSpeedFactor));
        }
    }

    private IEnumerator Fall()
    {
        int sprite = 0;
        while (sprite < fallSprites.Length)
        {
            (yin ? maskYin : maskYang).sprite = fallSprites[sprite];
            sprite++;
            yield return new WaitForSeconds(1f / (initialAnimationSpeed * animationSpeedFactor));
        }
    }
}
