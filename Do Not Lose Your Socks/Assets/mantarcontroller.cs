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
    SpriteRenderer SP�;
    Transform target;
    float speed = 1f;
    float starttoattack = 1;
    Transform kayna��;
    public Slider slite;
    public GameObject effectofdamage;
    int enemyhealth = 200;
    public AudioSource hitatma;
    public GameObject[] dieeffect;
    void Start()
    {
        //anima = GameObject.FindWithTag("Respawn").gameObject.GetComponent<Animator>();
        kayna�� = GameObject.FindWithTag("Respawn2").gameObject.GetComponent<Transform>();
        //kayna�� = GetComponent<Transform>();
        anima[0] = GameObject.FindWithTag("Player").gameObject.GetComponent<Animator>();
        //anima[1] = GameObject.FindWithTag("GameController").gameObject.GetComponent<Animator>();
        //anima[2] = GameObject.FindWithTag("Player").gameObject.GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SP� = GetComponent<SpriteRenderer>();
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
                SP�.flipX = true;
            }
            else if (target.position.x > transform.position.x)
            {
                SP�.flipX = false;
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
                    transform.position = Vector2.MoveTowards(transform.position, kayna��.position, speed * Time.deltaTime);
                }
                if (kayna��.position.x < transform.position.x)
                {
                    SP�.flipX = true;
                }
                else if (kayna��.position.x > transform.position.x)
                {
                    SP�.flipX = false;
                }

            }

        }
        print(sald�r�vurdu);
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
                SP�.flipX = true;
            }
            else if (target.position.x > transform.position.x)
            {
                SP�.flipX = false;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            rigi.bodyType = RigidbodyType2D.Dynamic;
        }
        if (sald�r�vurdu == true)
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
    bool sald�r�vurdu;
    bool yerinde;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            g�r�ld� = true;
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
            g�r�ld� = true;
        }
        else if (collision.gameObject.tag == "Finish")
        {
            sald�r�vurdu = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            g�r�ld� = false;

        }
        if (collision.gameObject.tag == "Finish")
        {
            sald�r�vurdu = false;
        }
        else if (collision.gameObject.tag == "Respawn2")
        {
            yerinde = false;
        }
    }
    bool g�r�ld� = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            g�r�ld� = true;
        }
        /*else if (collision.gameObject.tag == "Finish")
        {
            sald�r�vurdu = true;
        }*/
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /* if (collision.gameObject.tag == "Finish")
         {
             sald�r�vurdu = true;
         }*/
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            g�r�ld� = false;
        }
        /*else if (collision.gameObject.tag == "Finish")
        {
            sald�r�vurdu = false;
        }*/
    }
}
