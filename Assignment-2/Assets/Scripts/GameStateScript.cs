using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateScript : MonoBehaviour
{
    const float MIN_X = 27.85f;
    const float MIN_Y = -24.36f;
    private List<Card> unmatchedCards = new List<Card>();
    private Dictionary<string, GameObject> cardRefs = new Dictionary<string, GameObject>();
    private Card activeCard;
    private int countdown = 90;

    public AudioSource cardMatched;
    public AudioSource cheer;
    public AudioSource lose;
    public AudioSource countdownTick;

    public TextMeshPro countdownText;
    public GameObject cardPrefab;
    public bool isPlayable = false;

    void Awake()
    {
        Sprite[] cardImages = Resources.LoadAll<Sprite>("CardFaces");
        
        var cardList = new List<Card>();

        // Add 2 of each card to the list
        for (int i = 0; i < cardImages.Length; i++)
        {
            string id1 = cardImages[i].name + "1";
            string id2 = cardImages[i].name + "2";
            cardList.Add(new Card(id1, cardImages[i]));
            cardList.Add(new Card(id2, cardImages[i]));
        }

        // Shuffle the list
        var rand = new System.Random();
        var shuffledList = cardList.OrderBy(_ => rand.Next()).ToList();

        for (int i = 0; i < shuffledList.Count; i++)
        {
            int x = i % 4;
            int y = i / 4;
            float posX = MIN_X + (x * 2.8f);
            float posY = MIN_Y + (y * 2.8f);
            shuffledList[i].SetPosition(posX, posY);
            unmatchedCards.Add(shuffledList[i]);
        }
    }

    IEnumerator Start()
    {
        foreach (var card in unmatchedCards)
        {
            Vector3 pos = new Vector3(card.posX, card.posY, 0);
            var cardAdded = Instantiate(cardPrefab, pos, Quaternion.identity);
            cardAdded.name = card.id;
            cardRefs.Add(cardAdded.name, cardAdded);

            CardScript cs = cardAdded.GetComponentInChildren<CardScript>();
            cs.SetCardData(card.sprite, card.id);
            SetCardSize(cardAdded, 1.4f);
            yield return new WaitForSecondsRealtime(0.15f);
        }

        isPlayable = true;
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        bool isOutOfTime = countdown <= 0;
        bool isAllMatched = unmatchedCards.Count == 0;
        if ((isOutOfTime || isAllMatched) && isPlayable)
        {
            GameOver(isAllMatched);
        }
    }

    public void CardClicked(string id)
    {
        if (activeCard == null)
        {
            activeCard = unmatchedCards.Find(c => c.id == id);
            unmatchedCards.Remove(activeCard);
        }
        else
        {
            isPlayable = false;
            string idToMatch = id[..^1] + (id.EndsWith("1") ? "2" : "1");
            if (activeCard.id == idToMatch)
            {
                StartCoroutine(MatchAnimation(id));
            }
            else
            {
                StartCoroutine(ResetFlippedCards(id));
            }
        }
    }

    private void GameOver(bool isWin)
    {
        isPlayable = false;
        if (isWin)
        {
            StartCoroutine(HandleWin());
        }
        else
        {
            StartCoroutine(HandleLose());
        }
    }

    private void SetCardSize(GameObject card, float size)
    {
        card.transform.localScale = new Vector3(size, size, 0);
    }

    IEnumerator ResetFlippedCards(string id)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if (cardRefs.TryGetValue(activeCard.id, out GameObject cardObj1))
        {
            cardObj1.GetComponentInChildren<CardScript>().FlipCard();
        }

        if (cardRefs.TryGetValue(id, out GameObject cardObj2))
        {
            cardObj2.GetComponentInChildren<CardScript>().FlipCard();
        }

        yield return new WaitForSecondsRealtime(0.5f);
        unmatchedCards.Add(activeCard);
        activeCard = null;
        isPlayable = true;
    }


    IEnumerator MatchAnimation(string id)
    {
        cardRefs.TryGetValue(activeCard.id, out GameObject cardObj1);
        cardRefs.TryGetValue(id, out GameObject cardObj2);
        yield return new WaitForSecondsRealtime(0.75f);
        
        var card = unmatchedCards.Find(c => c.id == id);
        unmatchedCards.Remove(card);
        cardMatched.PlayOneShot(cardMatched.clip, 0.4f);
        cardObj1.GetComponentInChildren<CardScript>().HandleMatch();
        cardObj2.GetComponentInChildren<CardScript>().HandleMatch();
        yield return new WaitForSecondsRealtime(0.75f);
        
        activeCard = null;
        isPlayable = true;
    }

    IEnumerator Countdown()
    {
        while (countdown > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            countdown--;
            countdownText.text = countdown.ToString();

            if (countdown == 15 || countdown == 60)
            {
                countdownTick.PlayOneShot(countdownTick.clip, 0.5f);
            }

            if (countdown <= 10)
            {
                countdownText.color = (countdown % 2 == 0) ? Color.red : Color.white;
            }
        }
    }

    IEnumerator HandleWin()
    {
        cheer.PlayOneShot(cheer.clip, 0.5f);
        yield return new WaitForSecondsRealtime(2.5f);

        int time = 90 - countdown;
        int fastestTime = 90 - int.Parse(PlayerPrefs.GetString("FastestTime", "0"));

        if (time < fastestTime)
        {
            PlayerPrefs.SetString("FastestTime", time.ToString());
        }
        PlayerPrefs.SetString("GameResult", "win");
        PlayerPrefs.Save();

        StartCoroutine(LoadMenu());

    }

    IEnumerator HandleLose()
    {
        lose.PlayOneShot(lose.clip);
        yield return new WaitForSecondsRealtime(1.5f);

        PlayerPrefs.SetString("GameResult", "lose");
        PlayerPrefs.Save();
        
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        Scene menuScene = SceneManager.GetSceneByName("MenuScene");
        SceneManager.SetActiveScene(menuScene);
    }
}

public class Card
{
    public string id;
    public Sprite sprite;
    public float posX;
    public float posY;

    public Card(string id, Sprite sprite)
    {
        this.id = id;
        this.sprite = sprite;
    }

    public void SetPosition(float x, float y)
    {
        posX = x;
        posY = y;
    }
}
