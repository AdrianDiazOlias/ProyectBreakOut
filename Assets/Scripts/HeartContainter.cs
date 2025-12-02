using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartContainter : MonoBehaviour
{
    public float blinkDuration;

    [Header("Heart Sprites")]
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Sprite BlinkHeart;

    Image spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    public IEnumerator BlinkHeartAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.sprite = BlinkHeart;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.sprite = fullHeart;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.sprite = emptyHeart;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}


