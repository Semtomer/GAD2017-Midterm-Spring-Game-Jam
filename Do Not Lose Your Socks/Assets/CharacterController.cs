using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rigi;
    Animator anim;
    SpriteRenderer spi;
    Collider2D coli;
    float speed = 550;
    float jump = 0;
    float dashpower = 0;
    float jumptime;
    bool jumpokey = false;
    float dashduration;
    public static bool dashcheck = false;
    public Transform playerattackrage;
    public GameObject dashwindeffect;
    public Transform windtransfrom;
    public Collider2D[] bear;
    public AudioSource[] seseffectler;
    float triggeranim = 0;
    public Slider slo;
    public GameObject deadscreen;

    static public int health = 100;
    void Start()
    {
        bear[0] = GameObject.FindWithTag("Enemy").GetComponent<Collider2D>();
        bear[1] = GameObject.FindWithTag("Enemy1").GetComponent<Collider2D>();
        bear[2] = GameObject.FindWithTag("Enemy2").GetComponent<Collider2D>();
        bear[3] = GameObject.FindWithTag("Enemy3").GetComponent<Collider2D>();
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spi = GetComponent<SpriteRenderer>();
        coli = GetComponent<Collider2D>();
        health = (int)slo.maxValue;
    }

    float y;
    public static float attacktiming;
    float healing;
    float ayakseszamný;
    void Update()
    {
        slo.value = health;
        attacktiming += Time.deltaTime;
        healing += Time.deltaTime;
        if(healing > 1)
        {
            health++;
            healing = 0;
        }
        if(health >= 100)
        {
            health = 100;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
            deadscreen.SetActive(true);
            
        }
        //print(health);
        if (jumpokey == false)
        {
            y = Input.GetAxis("Vertical");
        }
        else
        {
            y = 0;
        }
        float x = Input.GetAxis("Horizontal");
        
        rigi.velocity = new Vector3(x, y, 0) * speed * Time.deltaTime;
        rigi.AddForce(transform.up * jump * Time.deltaTime, ForceMode2D.Impulse);
        rigi.AddForce(transform.right * dashpower * Time.deltaTime, ForceMode2D.Impulse);
        if (Input.GetKeyDown(KeyCode.X))
        {
            jumpokey = true;
            anim.SetBool("Jump", true);
            seseffectler[3].Play();
        }
        //dash
        if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
                anim.SetBool("Haraket", true);
                anim.SetFloat("Meco", 1);
            seseffectler[2].Play();
            dashcheck = true;
            if (bear[0] != null)
            {
                bear[0].isTrigger = true;
            }
            if (bear[1] != null)
            {
                bear[1].isTrigger = true;
            }
            if (bear[2] != null)
            {
                bear[2].isTrigger = true;
            }
            if (bear[3] != null)
            {
                bear[3].isTrigger = true;
            }


        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("Haraket", false);
            if (dashduration > 0.5f)
            {
                dashcheck = false;
            }
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(attacktiming > 0.4f)
            {
                anim.SetBool("Haraket", true);
                anim.SetFloat("Meco", 0);
                attacktiming = 0;
                seseffectler[0].Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("Haraket", false);
        }

        if (x > triggeranim || y < triggeranim)
        {
            if(jumpokey == false)
            {
                ayakseszamný += Time.deltaTime;
                if (ayakseszamný > 0.28)
                {
                    seseffectler[1].Play();
                    ayakseszamný = 0;
                }
            }
            anim.SetFloat("Moves", 1);
            spi.flipX = false;
            playerattackrage.localPosition = new Vector2(0.8f, 0);
            windtransfrom.localPosition = new Vector2(-0.8f, 0);
            if (dashcheck == true)
            {
                dashduration += Time.deltaTime;
                if (dashduration < 0.5f)
                {
                    dashpower = 1000;
                    dashwindeffect.SetActive(true);
                }
                else
                {
                    dashwindeffect.SetActive(false);
                    if (bear[0] != null)
                    {
                        bear[0].isTrigger = false;
                    }
                    if (bear[1] != null)
                    {
                        bear[1].isTrigger = false;
                    }
                    if (bear[2] != null)
                    {
                        bear[2].isTrigger = false;
                    }
                    if (bear[3] != null)
                    {
                        bear[3].isTrigger = false;
                    }
                    dashpower = 0;
                    dashduration = 0;
                    dashcheck = false;
                }
            }
        }
        else if(x < triggeranim || y > triggeranim)
        {
            anim.SetFloat("Moves", 1);
            spi.flipX = true;
            playerattackrage.localPosition = new Vector2(-0.8f, 0);
            windtransfrom.localPosition = new Vector2(0.8f, 0);
            if (dashcheck == true)
            {
                dashduration += Time.deltaTime;
                if (dashduration < 0.5f)
                {
                    dashpower = -1000;
                    dashwindeffect.SetActive(true);
                }
                else if(dashduration > 0.5f)
                {
                    dashwindeffect.SetActive(false);
                    if(bear[0] != null)
                    {
                        bear[0].isTrigger = false;
                    }
                    if(bear[1] != null)
                    {
                        bear[1].isTrigger = false;
                    }
                    if(bear[2] != null)
                    {
                        bear[2].isTrigger = false;
                    }
                    if(bear[3] != null)
                    {
                        bear[3].isTrigger = false;
                    }

                    dashpower = 0;
                    dashduration = 0;
                    dashcheck = false;
                }
            }
        }
        else
        {
            anim.SetFloat("Moves", 0);
        }

        if(jumpokey == true)
        {

            jumptime += Time.deltaTime;
            if (jumptime < 0.8f)   
            {
                coli.isTrigger = true;
                jump = 600;
            }
            else if(jumptime > 0.8f && jumptime <= 1.6f)
            {
                jump = -600;
            }
            else if(jumptime > 1.6f)
            {
                jump = 0;
                jumptime = 0;
                jumpokey = false;
                coli.isTrigger = false;
                anim.SetBool("Jump", false);

            }
            
        }
    }
    private void FixedUpdate()
    {
       
    }
}
