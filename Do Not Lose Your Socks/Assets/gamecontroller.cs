using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontroller : MonoBehaviour
{
    public GameObject victo;
    void Start()
    {
        
    }

    float passforother;
    void Update()
    {
        if(CharacterController.health <= 0)
        {
            passforother += Time.deltaTime;
            if (passforother > 2)
            {
                SceneManager.LoadScene(0);
            }
        }
        if(ba�ard�n == true)
        {
            victo.SetActive(true);
            passforother += Time.deltaTime;
            if (passforother > 2)
            {
                SceneManager.LoadScene(0);
            }
        }
       
        
    }
    bool ba�ard�n = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ba�ard�n = true;
        }
    }
}
