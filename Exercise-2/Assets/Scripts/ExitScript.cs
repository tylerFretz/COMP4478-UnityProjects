using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitScript : MonoBehaviour
{
    public GameObject button;
    public AudioSource buttonClick;
    public UnityEvent OnClick = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (button.GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                buttonClick.PlayOneShot(buttonClick.clip);
                OnClick.Invoke();
            }
        }
    }
}
