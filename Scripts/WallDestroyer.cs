using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Check for and destroy walls within the trigger collider on startup
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, transform.rotation.eulerAngles.z);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("PlayAreaWall"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
}