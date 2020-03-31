using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Coins instantiateCoins;
    private InstantiatePoop instantiatePoop;

    private StoreInformation storeInformation;
    // Use this for initialization
    void Start()
    {
        instantiatePoop = GameObject.FindGameObjectWithTag("MainScript").GetComponent<InstantiatePoop>();
        instantiateCoins = GameObject.FindGameObjectWithTag("MainScript").GetComponent<Coins>();
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            DeleteAll();
            this.transform.Find("Particle").GetComponent<ParticleSystem>().Play(true);
        }

        if (col.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "CoinB": { instantiatePoop.currentCoins += 10; break; }
                case "CoinS": { instantiatePoop.currentCoins += 40;  break; }
                case "CoinG": { instantiatePoop.currentCoins += 100; break; }
            }

            DeleteAll();
        }

    }

    void DeleteAll()
    {
        Debug.Log("ese ");
        this.GetComponent<Rigidbody2D>().isKinematic = true; //Si no usamos esto, el Poop se cae por el rigidbody y se llevara las particulas con el
        this.GetComponent<Rigidbody2D>().simulated = false;

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;

      //  instantiatePoop.poopsInScreen--;

        StartCoroutine(DestroyCoin(7));
    }

    public IEnumerator DestroyCoin(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject);
    }
}