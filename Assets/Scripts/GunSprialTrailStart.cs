using UnityEngine;

public class GunSprialTrailStart : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 10f;

    private int frameCounter;
    private int randomFrame;

    private void Start()
    {
        // Initialize the frame counter and set a random frame between 10 and 30.
        frameCounter = 0;
        randomFrame = Random.Range(10, 31);
    }

    private void Update()
    {
        // Increment the frame counter.
        frameCounter++;

        // Check if the current frame matches the random frame.
        if (frameCounter == randomFrame)
        {
            // Call the shoot function.
            shoot();

            // Reset the frame counter.
            frameCounter = 0;

            // Set a new random frame for the next shot.
            randomFrame = Random.Range(10, 31);
        }
    }

    private void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Apply speed to the bullet (use AddForce, setting y component to 0)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}