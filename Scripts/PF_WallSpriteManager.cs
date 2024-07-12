using UnityEngine;

public class PF_WallSpriteManager : MonoBehaviour
{
    // Stores multiple sprites for random look.
    public Sprite[] wallSprites;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        int rand = Random.Range(0, wallSprites.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = wallSprites[rand];
    }
}