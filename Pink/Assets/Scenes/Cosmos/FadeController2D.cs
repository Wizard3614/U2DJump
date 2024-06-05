using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController2D : MonoBehaviour
{
    public GameObject player;
    public float fadeDistance = 3f;
    public float fadeDuration = 0.5f;
    public GameObject targetObject;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFading = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Update()
    {
        if (spriteRenderer != null && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < fadeDistance && !isFading)
            {
                StartCoroutine(FadeOut());
            }
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Boom targetScript = targetObject.GetComponent<Boom>();
        targetScript.Trigge = true;
        spriteRenderer.color = endColor;
    }
}



