using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameStateScript gameState;
    public GameObject cardBack;
    public GameObject cardFace;
    public GameObject cardBackground;
    public AudioSource cardPlaced;
    public AudioSource cardFlipped;

    public string id;

    private bool isFaceUp = false;
    public float x, y, z;
    
    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameStateScript>();
        cardBack.SetActive(true);
        cardFace.SetActive(false);
        StartCoroutine(EntranceAnimation());
    }
    
    private void OnMouseDown()
    {
        if (gameState.isPlayable && !isFaceUp)
        {
            gameState.CardClicked(id);
            FlipCard();
        }
    }

    public void SetCardData(Sprite sprite, string id)
    {
        SpriteRenderer renderer = cardFace.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.size = new Vector2(2.45f, 2.55f);
        this.id = id;
    }

    public void FlipCard()
    {
        isFaceUp = !isFaceUp;
        SpriteRenderer renderer = cardBackground.GetComponent<SpriteRenderer>();
        renderer.color = isFaceUp ? Color.green : Color.black;
        StartCoroutine(FlipAnimation(0.5f));
    }

    public void HandleMatch()
    {
        StartCoroutine(FlipAnimation(1.5f));
        StartCoroutine(ShrinkAnimation(1.5f));
    }
    
    private void Flip()
    {
        cardFlipped.PlayOneShot(cardFlipped.clip, 0.2f);
        if (cardBack.activeSelf)
        {
            cardBack.SetActive(false);
            cardFace.SetActive(true);
        }
        else
        {
            cardBack.SetActive(true);
            cardFace.SetActive(false);
        }
    }

    IEnumerator EntranceAnimation()
    {
        float time = 0;
        while (time < 1)
        {
            time += (Time.deltaTime * 1.4f);
            transform.localScale = Vector3.Lerp(new Vector3(0.2f, 0.2f, 0.2f), Vector3.one, time);
            yield return null;
        }
    }

    IEnumerator FlipAnimation(float duration)
    {
        float time = 0f;
        float angle = 180f;
        bool flipped = false;
        Quaternion fromRotation = transform.rotation;
        Quaternion toRotation = transform.rotation * Quaternion.Euler(0, angle, 0);

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);

            if (!flipped && time > duration / 2)
            {
                Flip();
                flipped = true;
            }

            yield return null;
        }
    }

    IEnumerator ShrinkAnimation(float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
    
            float t = Mathf.Clamp01(time / duration);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return null;
        }

        Destroy(gameObject);
    }
}
