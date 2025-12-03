using UnityEngine;

public class BulletOrbit : MonoBehaviour
{
    private Transform playerTarget;
    public float orbitSpeed = 5f;
    public float moveSpeed = 3f; // Speed at which the bullets move outwards or inwards
    public float maxLifetime = 5f; // Destroy after a few seconds to clean up

    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    // Called by the Spawner script to set the player reference
    public void SetTarget(Transform target)
    {
        playerTarget = target;
    }

    void Update()
    {
        if (playerTarget != null)
        {
            // Make the bullet move outwards from the player over time
            // You can adjust this logic to fit specific "bullet hell" patterns
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, -moveSpeed * Time.deltaTime);

            // Optional: Rotate the bullet around the player
            // This rotation logic can be complex for true orbiting, the MoveTowards above handles basic movement away/towards
        }
        // If you want actual rotation around the player without moving away:
        // transform.RotateAround(playerTarget.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}

