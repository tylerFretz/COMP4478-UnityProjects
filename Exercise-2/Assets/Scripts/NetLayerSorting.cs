using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetLayerSorting : MonoBehaviour
{
    public SpriteRenderer netRenderer;
    public float offset = 0.4f;

    void LateUpdate()
    {
        int netSortingOrder = netRenderer.sortingOrder;
        GameObject[] collectableObjects = GameObject.FindGameObjectsWithTag("Collectables");
        foreach (GameObject collectable in collectableObjects)
        {
            SpriteRenderer collectableRenderer = collectable.GetComponent<SpriteRenderer>();
            float collectableX = collectable.transform.position.x;
            float netX = netRenderer.transform.position.x;
            float distance = Mathf.Abs(collectableX - netX);

            if (distance < offset)
            { 
                collectableRenderer.sortingOrder = netSortingOrder - 1;
            }
            else
            {
                collectableRenderer.sortingOrder = netSortingOrder + 1;
            }
        }
    }
}
