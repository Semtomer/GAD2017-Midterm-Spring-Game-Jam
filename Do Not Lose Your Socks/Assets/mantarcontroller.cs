using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mantarcontroller : MonoBehaviour
{
    Rigidbody2D rigi;
    Animator anim;
    public Animator[] anima;
    //public Animator animatio;
    SpriteRenderer SPÝ;
    Transform target;
    float speed = 1f;
    float starttoattack = 1;
    Transform kaynaðý;
    public Slider slite;
    public GameObject effectofdamage;
    int enemyhealth = 200;
    public AudioSource hitatma;
    public GameObject[] dieeffect;
    void Start()
    {
        //anima = GameObject.FindWithTag("Respawn").gameObject.GetComponent<Animator>();
        kaynaðý = GameObject.FindWithTag("Respawn2").gameObject.GetComponent<Transform>();
        //kaynaðý = GetComponent<Transform>();
        anima[0] = GameObject.FindWithTag("Player").gameObject.GetComponent<Animator>();
        //anima[1] = GameObject.FindWithTag("GameController").gameObject.GetComponent<Animator>();
        //anima[2] = GameObject.FindWithTag("Player").gameObject.GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SPÝ = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").gameObject.GetComponent<Transform>();
        //target = GetComponent<Transform>();
        enemyhealth = (int)slite.maxValue;
    }

    bool attackradius;
    bool detected;
    float detectradius = 4f;
    float radius = 0.5f;
    public LayerMask Player;
    float timeofit;
    public bool asd;
    void Update()
    {

        slite.value = enemyhealth;
        if (enemyhealth <= 0)
        {
            Destroy(gameObject);
            if (asd == true)
            {
                Instantiate<GameObject>(dieeffect[0], transform.position, Quaternion.identity);
            }
            else if (asd == false)
            {
                Instantiate<GameObject>(dieeffect[1], transform.position, Quaternion.identity);
            }

        }
        attackradius = Physics2D.OverlapCircle(transform.position, radius, Player);
        detected = Physics2D.OverlapCircle(transform.position, detectradius, Player);
        if (detected == true)
        {
            speed = 1;
            anim.SetFloat("Blend", 1);
            if (target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (target.position.x < transform.position.x)
            {
                SPÝ.flipX = true;
            }
            else if (target.position.x > transform.position.x)
            {
                SPÝ.flipX = false;
            }
        }
        else
        {

            if (yerinde == true)
            {
                anim.SetFloat("Blend", 0);
                speed = 0;
            }
            else if (yerinde == false)
            {
                anim.SetFloat("Blend", 1);
                if (target != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, kaynaðý.position, speed * Time.deltaTime);
                }
                if (kaynaðý.position.x < transform.position.x)
                {
                    SPÝ.flipX = true;
                }
                else if (kaynaðý.position.x > transform.position.x)
                {
                    SPÝ.flipX = false;
                }

            }

        }
        print(saldýrývurdu);
        starttoattack += Time.deltaTime;
        if (attackradius == true && CharacterController.dashcheck == false)
        {
            if (starttoattack > 2f)
            {
                CharacterController.health -= 10;
                anim.SetBool("Attack", true);
                starttoattack = 0;
                // anima[1].SetBool("Sallan", true);
                anima[0].SetBool("renk", true);
                hitatma.Play();
            }
            else
            {
                anim.SetBool("Attack", false);
                //anima[1].SetBool("Sallan", false);
                anima[0].SetBool("renk", false);
            }
            rigi.bodyType = RigidbodyType2D.Static;
            if (target.position.x < transform.position.x)
            {
                SPÝ.flipX = true;
            }
            else if (target.position.x > transform.position.x)
            {
                SPÝ.flipX = false;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            rigi.bodyType = RigidbodyType2D.Dynamic;
        }
        if (saldýrývurdu == true)
        {
            if (Input.GetKeyDown(KeyCode.Z) && CharacterController.attacktiming > 0.4f)
            {
                anim.SetBool("getdmg", true);
                //anima[0].SetBool("damage", true);
                enemyhealth -= 20;
                effectbool = true;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                anim.SetBool("getdmg", false);
                //anima[0].SetBool("damage", false);
            }
        }
        if (effectbool == true)
        {
            timeofit += Time.deltaTime;
            if (timeofit <= 0.4f)
            {
                effectofdamage.SetActive(true);
            }
            else if (timeofit > 0.4f)
            {
                effectofdamage.SetActive(false);
                effectbool = false;
                timeofit = 0;
            }
        }
        else if (attackradius == false)
        {
            anim.SetBool("getdmg", false);
        }

    }
    bool effectbool;
    bool saldýrývurdu;
    bool yerinde;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            görüldü = true;
        }
        else if (collision.gameObject.tag == "Respawn2")
        {
            yerinde = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            görüldü = true;
        }
        else if (collision.gameObject.tag == "Finish")
        {
            saldýrývurdu = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            görüldü = false;

        }
        if (collision.gameObject.tag == "Finish")
        {
            saldýrývurdu = false;
        }
        else if (collision.gameObject.tag == "Respawn2")
        {
            yerinde = false;
        }
    }
    bool görüldü = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            görüldü = true;
        }
        /*else if (collision.gameObject.tag == "Finish")
        {
            saldýrývurdu = true;
        }*/
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /* if (collision.gameObject.tag == "Finish")
         {
             saldýrývurdu = true;
         }*/
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            görüldü = false;
        }
        /*else if (collision.gameObject.tag == "Finish")
        {
            saldýrývurdu = false;
        }*/
    }
}
