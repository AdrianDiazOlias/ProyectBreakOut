using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartContainter : MonoBehaviour
{
    public float blinkDuration;
    public bool isBlinking = false;

    [Header("Heart Sprites")]
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Sprite BlinkHeart;
    public Sprite ShieldHeart;

    Image spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<Image>();
        SetHeartFull();
    }

    public IEnumerator BlinkHeartAnimation()
    {
        isBlinking = true;
        Debug.Log($"{this.gameObject.name} is blinking.");
        for (int i = 0; i < 3; i++)
        {
            SetHeartEmpty();
            yield return new WaitForSeconds(blinkDuration);
            SetHeartBlink();
            yield return new WaitForSeconds(blinkDuration);
            SetHeartFull();
            yield return new WaitForSeconds(blinkDuration);
        }
        isBlinking = false;
    }

    public void TriggerBlink()
    {
        if (!isBlinking)
            StartCoroutine(BlinkHeartAnimation());
    }

    public void SetHeartFull()
    {
        spriteRenderer.sprite = fullHeart;
    }

    public void SetHeartEmpty()
    {
        spriteRenderer.sprite = emptyHeart;
    }

    public void SetHeartBlink()
    {
        spriteRenderer.sprite = BlinkHeart;
    }

    public void SetHeartShield()
    {
        spriteRenderer.sprite = ShieldHeart;
    }
}