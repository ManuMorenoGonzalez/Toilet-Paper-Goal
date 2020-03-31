using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopController : MonoBehaviour {
    private InstantiatePoop instantiatePoop;
    public StoreInformation storeInformation;

    public int totalGoals = 0;
    private bool sumGoal = false;

    // Use this for initialization
    void Start () {
        instantiatePoop = GameObject.FindGameObjectWithTag("MainScript").GetComponent<InstantiatePoop>();
        storeInformation = GameObject.FindGameObjectWithTag("StoreInformation").GetComponent<StoreInformation>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            DeleteAll();
            this.transform.Find("Particle").GetComponent<ParticleSystem>().Play(true);
           
        }

        if (col.tag == "Goal" && !sumGoal)
        {
            storeInformation.totalGoals++;
            instantiatePoop.poopsGoal++;

            sumGoal = true;
            DeleteAll();
            storeInformation.coins += 20;
            instantiatePoop.currentCoins += 20;
            instantiatePoop.totalPoopsGoal++;
            this.transform.Find("Goal").GetComponent<ParticleSystem>().Play(true);
        }

    }

    void DeleteAll()
    {
        Debug.Log("ese ");
        this.GetComponent<Rigidbody2D>().isKinematic = true; //Si no usamos esto, el Poop se cae por el rigidbody y se llevara las particulas con el
        this.GetComponent<Rigidbody2D>().simulated = false;

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;

        instantiatePoop.poopsInScreen--;

        StartCoroutine(DestroyPoop(7));
    }

    public IEnumerator DestroyPoop(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject);
    }
}