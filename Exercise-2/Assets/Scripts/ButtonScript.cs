using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public GameObject button;
    public GameObject buttonPressed;
    public UnityEvent OnClick = new UnityEvent();
    public AudioSource buttonClick;

    private void Update()
    {
        // If the button is pressed, the button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (button.GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                buttonClick.PlayOneShot(buttonClick.clip);
                button.SetActive(false);
                buttonPressed.SetActive(true);
                OnClick.Invoke();
            }
        }
        // If the button is released, the button is released
        else if (Input.GetMouseButtonUp(0))
        {
            button.SetActive(true);
            buttonPressed.SetActive(false);
        }
    }
}
