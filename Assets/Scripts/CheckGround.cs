using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

    private PlayerMove player;
    public bool test = true;
	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerMove>();
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") player.grounded = true;
        test = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") player.grounded = false;
        test = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
