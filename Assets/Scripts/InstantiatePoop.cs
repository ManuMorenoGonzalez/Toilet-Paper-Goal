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

    // Use this for initialization
    void Start()
    {
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();

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
            
            Debug.Log("poopsPlayedInThisLevel "+ poopsPlayedInThisLevel);

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
                    Debug.Log("currentLevel " + currentLevel);
                    maxPoopInLevel = 4;
                    poopsGoalNextLevel = 2;
                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        Loser();
                        storeInformation.Save();
                    }
                    if (poopsGoal == poopsGoalNextLevel)
                    {
                        currentCoins += 50;
                        storeInformation.coins += 50;
                        currentLevel++;
                        poopsPlayedInThisLevel = 0;
                        poopsGoal = 0;
                        storeInformation.Save();
                    }
                    break;
                }
            case 2:
                {
                    maxPoopInLevel = 8;
                    poopsGoalNextLevel = 4;

                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        Loser();
                        storeInformation.Save();
                    }
                    if (poopsGoal == poopsGoalNextLevel)
                    {
                        currentCoins += 300;
                        storeInformation.coins += 300;
                        currentLevel++;
                        poopsPlayedInThisLevel = 0;
                        poopsGoal = 0;
                        storeInformation.Save();
                    }
                    break;
                }
            case 3:
                {
                    maxPoopInLevel = 10;
                    poopsGoalNextLevel = 7;

                    if (poopsPlayedInThisLevel > maxPoopInLevel)
                    {
                        storeInformation.Save();
                        Loser();
                    }
                    if (poopsGoal == poopsGoalNextLevel && !isWinner)
                    {
                        //WIN
                        isWinner = true;
                        currentCoins += 1000;
                        storeInformation.coins += 1000;
                        storeInformation.Save();
                        Winner();
                    }
                    break;
                }
        }
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
}