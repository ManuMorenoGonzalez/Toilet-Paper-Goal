using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreInformation : MonoBehaviour
{
    public int record;
    public int totalGoals;
    public int coins;

    private String routeFile;
    public static StoreInformation storeInformation;

    public Text textScore;
    public Text textCoins;

    //SHOP
    public bool plunger;
    public bool shower;
    public bool mirrow;
    public bool tower;
    public bool sink;

    private void Awake()
    {
        //los datos se almacenarán en el dispositivo al nombre de savedViniciusRunner.gd
        routeFile = Application.persistentDataPath + "/savedToiletPaperGoal.gd";
        if (storeInformation == null)
        {
            storeInformation = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (storeInformation != this)
        {
            Destroy(gameObject);
        }

    }

    // Use this for initialization
    void Start()
    {
        // globalSpeed = 7f;
        // slider.maxValue = maxValueSlider;
        Load();
    }

    void Update()
    {
       /* if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            textScore.text = scoreLastGame.ToString();
            textCoins.text = coinsLastGame.ToString();

            slider.maxValue = storeInformation.maxValueSlider;
            slider.value = scoreLastGame;

            if (scoreLastGame > 10 && scoreLastGame <= 20) globalSpeed = 8.5f;
            else if (scoreLastGame > 20 && scoreLastGame <= 30) globalSpeed = 9;
            else if (scoreLastGame > 30 && scoreLastGame <= 40) globalSpeed = 10;
            else if (scoreLastGame > 40 && scoreLastGame <= 50) globalSpeed = 11;
            else if (scoreLastGame > 50 && scoreLastGame <= 70) globalSpeed = 12;
            else if (scoreLastGame > 70 && scoreLastGame <= 90) globalSpeed = 12.5f;
            else if (scoreLastGame > 90) globalSpeed = 13;
        }*/
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(routeFile);
        DataSave datas = new DataSave();

        datas.totalGoals = totalGoals;
        datas.record = record;
        datas.coins = coins;
     
        //SHOP
        datas.plunger = plunger;
        datas.shower = shower;
        datas.mirrow = mirrow;
        datas.tower = tower;
        datas.sink = sink;

        bf.Serialize(file, datas);
        file.Close();
    }


    void Load()
    {
        if (File.Exists(routeFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(routeFile, FileMode.Open);

            DataSave datas = (DataSave)bf.Deserialize(file);

            totalGoals = datas.totalGoals;
            record = datas.record;
            coins = datas.coins;

            //SHOP
            plunger = datas.plunger;
            shower = datas.shower;
            mirrow = datas.mirrow;
            tower = datas.tower;
            sink = datas.sink;

            file.Close();
        }
        else
        {
            totalGoals = 0;
            record = 0;
            coins = 0;

            //    contDefenders = 0;
            plunger = true;
            shower = false;
            mirrow = false;
            tower = false;
            sink = false;
        }
    }


    [Serializable]

    class DataSave
    {
        public int totalGoals;
        public int record;
        public int coins;
        public int scoreLastGame;
        public int coinsLastGame;
        //   public int contDefenders;
        //SHOP
        public bool plunger;
        public bool shower;
        public bool mirrow;
        public bool tower;
        public bool sink;
    }
}
