using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HellHunter : MonoBehaviour
{
    public static HellHunter Instance;
    public Transform[] spawnPoints;
    public GameObject duckPrefab;
    public SpriteRenderer bg;
    public Color blackColor;

    public GameObject hellHoundMiss, hellHoundHit;
    public SpriteRenderer hellHoundSprite;
    public Sprite[] victorySprites;

    public TextMeshProUGUI roundText;
    int roundNumber = 1;
     public int totalTrials = 10;

    public int totalHits;
    int ducksCreated;
    bool isRoundOver;

    public TextMeshProUGUI scoreText, hitsText;
    int score, hits, totalClicks;




    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            totalClicks++;

        scoreText.text = score.ToString("000");
        hitsText.text = hits.ToString() + "/" + totalClicks.ToString();
    }

    public void CallCreateDucks()
    {
        StartCoroutine(CreateDucks(2));
    }

    IEnumerator CreateDucks(int _count)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < _count; i++)
        {
            GameObject _duck = Instantiate(duckPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            ducksCreated++;
        }

        StartCoroutine(TimeUp());
    }
    IEnumerator TimeUp()
    {
        yield return new WaitForSeconds(30f);
        DuckBehavior[] ducks = FindObjectsOfType<DuckBehavior>();
        for (int i = 0; i < ducks.Length; i++)
        {
            ducks[i].Timeup();
        }
        bg.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        bg.color = blackColor;
        if (!isRoundOver)
            StartCoroutine(RoundOver());
    }

    public void HitDuck()
    {
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        totalHits++;
        score += 10;
        hits++;
        bg.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        bg.color = blackColor;
        ducksCreated--;

        if (ducksCreated <= 0)
        {
            if (!isRoundOver)
            {
                StopCoroutine(TimeUp());
                StartCoroutine(RoundOver()); 
            }
        }
        
    }

    IEnumerator RoundOver()
    {
        isRoundOver = true;
        yield return new WaitForSeconds(1f);

        if (ducksCreated <= 0)
        {
            hellHoundHit.SetActive(true);
            hellHoundSprite.sprite = victorySprites[0];
        }
        else if (ducksCreated == 1)
        {
            hellHoundHit.SetActive(true);
            hellHoundSprite.sprite = victorySprites[1];
        }
        else 
        {
            hellHoundMiss.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        hellHoundHit.SetActive(false);
        hellHoundMiss.SetActive(false);
        CallCreateDucks();
        isRoundOver = false;
    }
}

