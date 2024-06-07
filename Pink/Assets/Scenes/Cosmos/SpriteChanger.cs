using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    
    public bool Trigge = false; 
    public bool Trigge2 = false; 

    public Sprite newSprite; 


    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;

    public Transform targetDestination; 
    public Object thisobject;


    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {

            originalSprite = spriteRenderer.sprite;
        }
        
    }

    void Update()
    {
        if (Trigge && newSprite != null)
        {
            ChangeSprite();
        }
        else if (!Trigge && originalSprite != null)
        {
            ResetSprite();
        }
        if (Trigge2 )
        {
            Destroy(thisobject);
        }
    }

    void ChangeSprite()
    {
        if (spriteRenderer.sprite != newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    void ResetSprite()
    {
        if (spriteRenderer.sprite != originalSprite)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Trigge)
        {
            other.transform.position = targetDestination.position;
        }
    }
}
