using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BombMovementScript : MonoBehaviour
{
    public GameStateScript gameState;
    public Rigidbody2D rb;
    public float speed = 18.5f;
    public float deadZone = -14;
    public float rotationSpeed = 250;
    public AudioSource explosionSound;
    public GameObject explosionPrefab;

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameStateScript>();
        
        // Ignore collisions with other fish / bombs
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Collectables"), LayerMask.NameToLayer("Collectables"));

        GameObject net = GameObject.FindGameObjectWithTag("Net");
        Vector2 direction = (net.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        rb.angularVelocity = rotationSpeed;

        // Destroy the bomb if it's out of the screen
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Net"))
        {
            HitNet();
        }
    }

    private async Task HitNet()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Vector3 explosionPosition = transform.position;
        explosionSound.PlayOneShot(explosionSound.clip);
        explosionPrefab = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        await Explode();
        gameState.GameOver();
    }

    private async Task Explode()
    {
        float growthDuration = 0.9f;
        float growthSpeed = 2.5f / growthDuration;
        float elapsedTime = 0f;

        while (elapsedTime < growthDuration)
        {
            float scale = Mathf.Lerp(0, 2.5f, elapsedTime * growthSpeed);
            explosionPrefab.transform.localScale = new Vector3(scale, scale, scale);
            await Task.Delay(10);
            elapsedTime += 0.01f;
        }
    }
}
