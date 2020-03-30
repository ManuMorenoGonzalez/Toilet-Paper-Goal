using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public StoreInformation storeInformation;
    //  public MainScript mainScript;
    public Text txtCurrentCoins;

    //Shop
    public bool plunger;
    public bool shower;
    public bool mirrow;
    public bool tower;
    public bool sink;

    public Button plungerBt;
    public Button showerBt;
    public Button mirrowBt;
    public Button towerBt;
    public Button sinkBt;

    // Use this for initialization
    void Start()
    {
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();
        //mainScript = GameObject.FindGameObjectWithTag("MainGameObject").GetComponent<MainScript>();
        //Esto se hace para ver si se muestra como comprado o no en la tienda
        plunger = storeInformation.plunger;
        shower = storeInformation.shower;
        mirrow = storeInformation.mirrow;
        sink = storeInformation.sink;
        tower = storeInformation.tower;

    }

    // Update is called once per frame
    void Update()
    {
        txtCurrentCoins.text = storeInformation.coins.ToString();

        if (!mirrow)
        {
            if (storeInformation.coins >= 10000)
                mirrowBt.GetComponent<Image>().color = Color.green;
            else
                mirrowBt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            mirrowBt.interactable = false;
            mirrowBt.GetComponent<Image>().color = Color.black;
        }

        if (!plunger)
        {
            if (storeInformation.coins >= 6000)
                plungerBt.GetComponent<Image>().color = Color.green;
            else
                plungerBt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            plungerBt.interactable = false;
            plungerBt.GetComponent<Image>().color = Color.black;
        }

        if (!shower)
        {
            if (storeInformation.coins >= 15000)
                showerBt.GetComponent<Image>().color = Color.green;
            else
                showerBt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            showerBt.interactable = false;
            showerBt.GetComponent<Image>().color = Color.black;
        }

        if (!tower)
        {
            if (storeInformation.coins >= 7000)
                towerBt.GetComponent<Image>().color = Color.green;
            else
                towerBt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            towerBt.interactable = false;
            towerBt.GetComponent<Image>().color = Color.black;
        }

        if (!sink)
        {
            if (storeInformation.coins >= 8000)
                sinkBt.GetComponent<Image>().color = Color.green;
            else
                sinkBt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            sinkBt.interactable = false;
            sinkBt.GetComponent<Image>().color = Color.black;
        }
    }

    public void shopBuy()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "ButtonPlunger":
                if (storeInformation.coins >= 6000)
                {
                    plungerBt.GetComponent<Image>().color = Color.green;
                    storeInformation.coins -= 6000;
                    storeInformation.coins = storeInformation.coins;
                    plunger = true;
                    storeInformation.plunger = plunger;
                    plungerBt.interactable = false;
                    storeInformation.Save();
                }
                else
                    plungerBt.GetComponent<Image>().color = Color.red;
                break;
            case "ButtonShower":
                if (storeInformation.coins >= 15000)
                {
                    showerBt.GetComponent<Image>().color = Color.green;
                    storeInformation.coins -= 15000;
                    storeInformation.coins = storeInformation.coins;
                    shower = true;
                    storeInformation.shower = shower;
                    showerBt.interactable = false;
                    storeInformation.Save();
                }
                else
                    showerBt.GetComponent<Image>().color = Color.red;
                break;
            case "ButtonMirrow":
                if (storeInformation.coins >= 10000)
                {
                    mirrowBt.GetComponent<Image>().color = Color.green;
                    storeInformation.coins -= 10000;
                    storeInformation.coins = storeInformation.coins;
                    mirrow = true;
                    storeInformation.mirrow = mirrow;
                    mirrowBt.interactable = false;
                    storeInformation.Save();
                }
                else
                    mirrowBt.GetComponent<Image>().color = Color.red;
                break;
            case "ButtonSink":
                if (storeInformation.coins >= 6000)
                {
                    sinkBt.GetComponent<Image>().color = Color.green;
                    storeInformation.coins -= 6000;
                    storeInformation.coins = storeInformation.coins;
                    sink = true;
                    storeInformation.sink = sink;
                    sinkBt.interactable = false;
                    storeInformation.Save();
                }
                else
                    sinkBt.GetComponent<Image>().color = Color.red;
                break;
            case "ButtonTower":
                if (storeInformation.coins >= 7000)
                {
                    towerBt.GetComponent<Image>().color = Color.green;
                    storeInformation.coins -= 7000;
                    storeInformation.coins = storeInformation.coins;
                    tower = true;
                    storeInformation.tower = tower;
                    towerBt.interactable = false;
                    storeInformation.Save();
                }
                else
                    towerBt.GetComponent<Image>().color = Color.red;
                break;
        }
    }
}
