using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float maxSpeed = 3f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 0.1f;
    public float attackPower = 2.5f;

    private Vector2 direcction = Vector2.right;
    private Rigidbody2D rb2dPlayer;
    private Animator animPlayer;
    public bool jump = false;
    public bool attack = false;
    public bool damaged = false;
    public bool fisrtAttack = false;
    public bool fisrtDamaged = false;
    private float directionJump = 0f;



	// Use this for initialization
	void Start () {
        rb2dPlayer = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        animPlayer.SetFloat("Walk", Mathf.Abs(rb2dPlayer.velocity.x));
        animPlayer.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !fisrtAttack)
        {
            attack = true;
            fisrtAttack = true;
            this.transform.Find("AttackSection").gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W) && !fisrtDamaged)
        {
            damaged = true;
            fisrtDamaged = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h > 0f && !fisrtDamaged)
        {
            direcction = Vector2.right;
            transform.localScale = new Vector3(30f, 30f, 30f);
            rb2dPlayer.velocity = new Vector2(speed, rb2dPlayer.velocity.y);
        }
        if (h < 0f && !fisrtDamaged)
        {
            direcction = Vector2.left;
            transform.localScale = new Vector3(-30f, 30f, 30f);
            rb2dPlayer.velocity = new Vector2(-speed, rb2dPlayer.velocity.y);
        }

        if (jump && !fisrtDamaged)
        {
            rb2dPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if (attack && !fisrtDamaged)
        { 
            animPlayer.SetBool("Attack", true);
            speed *= attackPower;
            rb2dPlayer.AddForce(direcction * speed, ForceMode2D.Impulse);
            StartCoroutine(AttackStop(0.50f));  
        }

        if (damaged)
        {
            animPlayer.SetBool("Damaged", true);
            speed *= attackPower;
            rb2dPlayer.AddForce(direcction * -4, ForceMode2D.Impulse);
            StartCoroutine(DamagedStop(0.50f));
        }


        Debug.Log(grounded);

    }

    public IEnumerator AttackStop(float tiempo)
    {
        attack = false;
        yield return new WaitForSeconds(tiempo);
        speed /= attackPower;
        fisrtAttack = false;
        this.transform.Find("AttackSection").gameObject.SetActive(false);
        animPlayer.SetBool("Attack", false);
    }

    public IEnumerator DamagedStop(float tiempo)
    {
        damaged = false;
        yield return new WaitForSeconds(tiempo);
        speed /= attackPower;
        fisrtDamaged = false;
        animPlayer.SetBool("Damaged", false);
    }

}
