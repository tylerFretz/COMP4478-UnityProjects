using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerScript : MonoBehaviour
{
    public GameObject fishPrefab;
    public float heightOffset = 3.0f;

    private float spawnInterval = 1;
    private float spawnTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        fishPrefab.layer = LayerMask.NameToLayer("Collectables");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnInterval)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnInterval = Random.Range(2.5f, 5.5f);
            spawnTimer = 0;
            spawnFish();
        }
    }

    void spawnFish()
    {
        float randomY = Random.Range(-heightOffset, heightOffset);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
        Instantiate(fishPrefab, spawnPosition, Quaternion.Euler(0, 0, -90));
    }
}
