using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public GameObject flashPrefab;

    private bool hasFired = false;
    private bool isFiring = false;
    public AudioSource movingSound;
    public AudioSource firingSound;

    void Start()
    {
        cannonBallPrefab.layer = LayerMask.NameToLayer("Attacking");
        movingSound.Play();
    }


    void Update()
    {
        // Moving into position
        if (transform.position.x > 9.5 && !hasFired && !isFiring)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        // Retreating
        else if (transform.position.x < 12.5 && hasFired && !isFiring)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        // In firing position
        else if (transform.position.x <= 9.5 && !hasFired && !isFiring)
        {
            movingSound.Pause();
            isFiring = true;
            StartCoroutine(FireCannon());
        }
        // My job is done here
        else if (!isFiring && hasFired)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator FireCannon()
    {
        // Wait for a few seconds to juke the player
        yield return new WaitForSeconds(Random.Range(1.0f, 2.5f));

        // Fire cannon
        Vector3 bombSpawnPos = new Vector3(transform.position.x - 1.25f, transform.position.y + 0.54f, transform.position.z);
        Vector3 flashSpawnPos = new Vector3(transform.position.x - 2.13f, transform.position.y + 1.04f, transform.position.z);
        Instantiate(cannonBallPrefab, bombSpawnPos, Quaternion.identity);
        flashPrefab = Instantiate(flashPrefab, flashSpawnPos, Quaternion.identity);
        firingSound.Play();

        Destroy(flashPrefab, 0.2f);

        yield return new WaitForSeconds(1);
        isFiring = false;
        hasFired = true;
        movingSound.UnPause();
    }
}
