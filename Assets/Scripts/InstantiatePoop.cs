using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstantiatePoop : MonoBehaviour
{
    public int poopsInScreen = 0;
    public int poopsPlayedInThisLevel = 0;
    public int poopsGoal = 0;
    public int poopsGoalNextLevel = 2;
    public bool isWinner = false;
    public int totalPoopsGoal = 0;

    public int maxPoopInLevel = 4;
    private int maxPoopInScreen = 1;
    public int currentLevel = 1;
    public int currentCoins = 0;

    public Text txtCurrentLevel;
    public Text txtCurrentCoins;
    public Text txtNextLevel;
    public Text txtPoopsGoal;
    public Text txtNumberPoops;

    public Text txtCurrentCoinsSummaryLoser;
    public Text txtPoopsGoalSummaryLoser;
    public Text txtCurrentCoinsSummaryWinner;
    public Text txtPoopsGoalSummaryWinner;

    public GameObject poop;
    public StoreInformation storeInformation;

    public GameObject AllObject;
    public GameObject LoserPanel;
    public GameObject WinnerPanel;

    //public GameObject[] objects;                // The prefab to be spawned.
    //public float spawnTime = 2f;            // How long between each spawn.
    private Vector3 spawnPosition;

    public GameObject plungerItem;
    public GameObject showerItem;
    public GameObject mirrowItem;
    public GameObject towerItem;
    public GameObject sinkItem;

    public GameObject coinB;
    public GameObject coinS;
    public GameObject coinG;
    public float timeCoins = 3;

    // Use this for initialization
    void Start()
    {
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();
        InvokeRepeating("InstantiateCoins", 20.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // poopsPlayedInThisLevel++;

        CheckBuyItems();

        txtCurrentLevel.text = currentLevel.ToString();
        txtCurrentCoins.text = currentCoins.ToString();
        txtNextLevel.text = poopsGoalNextLevel.ToString();
        txtPoopsGoal.text = poopsGoal.ToString();
        txtNumberPoops.text = (maxPoopInLevel - poopsPlayedInThisLevel).ToString();

        txtCurrentCoinsSummaryLoser.text = currentCoins.ToString();
        txtPoopsGoalSummaryLoser.text = totalPoopsGoal.ToString();
        txtCurrentCoinsSummaryWinner.text = currentCoins.ToString();
        txtPoopsGoalSummaryWinner.text = totalPoopsGoal.ToString();

         if (totalPoopsGoal > storeInformation.record) storeInformation.record = totalPoopsGoal;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        Instantiate();
      //  NextLevel();
    }

    void CheckBuyItems()
    {
        if(storeInformation.mirrow) mirrowItem.SetActive(true);
        if (storeInformation.plunger) plungerItem.SetActive(true);
        if (storeInformation.shower) showerItem.SetActive(true);
        if (storeInformation.tower) towerItem.SetActive(true);
        if (storeInformation.sink) sinkItem.SetActive(true);
    }

    //Instanciamos el Monster en pantalla y le damos una posicion aleatoria en la pantalla
    void Instantiate()
    {
        if (poopsInScreen < maxPoopInScreen)
        {
            poopsPlayedInThisLevel++;
            poopsInScreen++;
            
            spawnPosition.x = Random.Range(-Screen.width/10, Screen.width/10);
            spawnPosition.y = -20f;
            spawnPosition.z = 0f;
 
            GameObject monster = Instantiate(poop, spawnPosition, Quaternion.identity) as GameObject;
            monster.transform.SetParent(GameObject.FindGameObjectWithTag("Poops").transform, false);

        }

        switch (currentLevel)
        {
            case 1:
                {
                    maxPoopInLevel = 4;
                    poopsGoalNextLevel = 2;
                    if (poopsGoal == poopsGoalNextLevel)
                    {
                        currentCoins += 50;
                        storeInformation.coins += 50;
                        currentLevel++;
                        timeCoins = 3;
                        poopsPlayedInThisLevel = 0;
                        poopsGoal = 0;
                        storeInformation.Save();
                    }
                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        Loser();
                        storeInformation.Save();
                    }
                    break;
                }
            case 2:
                {
                    maxPoopInLevel = 8;
                    poopsGoalNextLevel = 5;
                    if (poopsGoal == poopsGoalNextLevel)
                    {
                        currentCoins += 300;
                        storeInformation.coins += 300;
                        currentLevel++;
                        timeCoins = 3;
                        poopsPlayedInThisLevel = 0;
                        poopsGoal = 0;
                        storeInformation.Save();
                    }
                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        Loser();
                        storeInformation.Save();
                    }
                    break;
                }
            case 3:
                {
                    maxPoopInLevel = 10;
                    poopsGoalNextLevel = 7;
                    if (poopsGoal == poopsGoalNextLevel && !isWinner)
                    {
                        //WIN
                        isWinner = true;
                        currentCoins += 1000;
                        storeInformation.coins += 1000;
                        storeInformation.Save();
                        Winner();
                    }
                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        storeInformation.Save();
                        Loser();
                    }
                    break;
                }
        }
    }

    void InstantiateCoins()
    {
        if (timeCoins > 0)
        {
            timeCoins--;
            spawnPosition.x = Random.Range(-Screen.width / 10, Screen.width / 10);
            spawnPosition.y = -20f;
            spawnPosition.z = 0f;


            switch (currentLevel)
            {
                case 1:
                    {
                        GameObject coin = Instantiate(coinB, spawnPosition, Quaternion.identity) as GameObject;
                        coin.transform.SetParent(GameObject.FindGameObjectWithTag("Poops").transform, false);
                        break;
                    }
                case 2:
                    {
                        GameObject coin = Instantiate(coinS, spawnPosition, Quaternion.identity) as GameObject;
                        coin.transform.SetParent(GameObject.FindGameObjectWithTag("Poops").transform, false);
                        break;
                    }
                case 3:
                    {
                        GameObject coin = Instantiate(coinG, spawnPosition, Quaternion.identity) as GameObject;
                        coin.transform.SetParent(GameObject.FindGameObjectWithTag("Poops").transform, false);
                        break;
                    }
            }
        }
    }

    public IEnumerator GenerateNewCoin(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject);
    }

    void Loser()
    {
        AllObject.SetActive(false);
        LoserPanel.SetActive(true);
    }

    void Winner()
    {
        AllObject.SetActive(false);
        WinnerPanel.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}