using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovementScript : MonoBehaviour
{
    public GameStateScript gameState;
    public Rigidbody2D rb;
    public int scoreValue = 1;
    public float deadZone = -9;
    public float waterSurfaceY = -2.5f;
    public float rotationSpeed = 400;
    public AudioSource flappingSound;
    public float moveSpeed;
    public float waveAmplitude;
    public float waveOffset;
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Ignore collisions with other fish / bombs
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Collectables"), LayerMask.NameToLayer("Collectables"));

        // Randomize individual fish attributes
        moveSpeed = Random.Range(1.2f, 4.3f);
        waveAmplitude = Random.Range(0.05f, 0.65f);
        waveOffset = Random.Range(0, 2 * Mathf.PI);

        spriteRenderer = GetComponent<SpriteRenderer>();
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameStateScript>();
    }

    void Update()
    {
        // Calculate the fish's new position based on the wave pattern
        float x = transform.position.x - (moveSpeed * Time.deltaTime);
        float y = transform.position.y + waveAmplitude * Mathf.Sin((x * moveSpeed) + waveOffset);
        Vector2 velocity = new Vector2(-moveSpeed, y - transform.position.y);

        rb.velocity = velocity;

        // Calculate the fish's new rotation to face its movement direction
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.AngleAxis(180 - angle, Vector3.back);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Destroy the fish if it's out of the screen
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        // If the fish is almost at the surface, push it back down
        if (transform.position.y > waterSurfaceY)
        {
            Vector2 pushDirecton = new Vector2(0, -2);
            rb.AddForce(pushDirecton, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Net") && IsInNet())
        {
            StartCoroutine(CaughtInNet());
        }
    }

    private bool IsInNet()
    {
        var net = GameObject.FindGameObjectWithTag("Net");
        float distance = Vector2.Distance(transform.position, net.transform.position);
        return distance < 0.4f;
    }

    IEnumerator CaughtInNet()
    {
        flappingSound.PlayOneShot(flappingSound.clip);

        // Rotate the fish back and forth to simulate it struggling in the net
        Quaternion originalRotation = transform.rotation;
        float rotateRange = 15f;
        float rotateStartTime = Time.time;
        int rotationCount = 0;
        while (rotationCount < 4)
        {
            float t = (Time.time - rotateStartTime);
            float rotateAngle = Mathf.Sin(t * Mathf.PI * 10f) * rotateRange;
            Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
            transform.rotation = originalRotation * rotation;
            if (t >= 0.35f)
            {
                rotationCount++;
                rotateStartTime = Time.time;
            }
            yield return null;
        }

        // Check if still in net
        if (IsInNet())
        {
            gameState.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
