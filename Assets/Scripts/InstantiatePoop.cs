using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePoop : MonoBehaviour
{

    public int poopsInScreen = 0;
    private int maxPoopInScreen = 1;

    public List<GameObject> poopSelected;

    //public GameObject[] objects;                // The prefab to be spawned.
    //public float spawnTime = 2f;            // How long between each spawn.
    private Vector3 spawnPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        Instantiate();
    }

    //Instanciamos el Monster en pantalla y le damos una posicion aleatoria en la pantalla
    void Instantiate()
    {
        if (poopsInScreen < maxPoopInScreen)
        {
            poopsInScreen++;
            int rand = Random.Range(0, poopSelected.Count-1);


            spawnPosition.x = Random.Range(-Screen.width/10, Screen.width/10);
            spawnPosition.y = -20f;
            spawnPosition.z = 0f;
 

            GameObject monster = Instantiate(poopSelected[rand], spawnPosition, Quaternion.identity) as GameObject;
            monster.transform.SetParent(GameObject.FindGameObjectWithTag("Poops").transform, false);
            poopSelected.RemoveAt(rand);




            //Basicamente cogemos el ancho alto de la pantalla y le restamos la anchura y altura del enemigo
            //para que nunca sobre salga de la pantalla. (el /3 del transfor es porque quizas es demasiado espacio que se le da)
            /*     Vector2 transformPosition = new Vector2(
                         Random.Range(-horizontal / 3f + monstersSelected[rand].transform.localScale.x, horizontal / 3f
                         - monstersSelected[rand].transform.localScale.x),
                         Random.Range(-vertical / 3.2f + monstersSelected[rand].transform.localScale.y, vertical / 3.2f
                         - monstersSelected[rand].transform.localScale.y));

                     //Una vez tenemos ya la posicion en pantalla, instanciamos el objeto dentro del GameObject "Monsters"
                     //y lo eliminamos del array de monsterSelected.
                     GameObject monster = Instantiate(monstersSelected[rand], transformPosition, transform.rotation) as GameObject;
                     monster.transform.SetParent(GameObject.FindGameObjectWithTag("Monsters").transform, false);
                     monstersSelected.RemoveAt(rand);
                     */
        }
    }
}