using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public float followspeed = 2f;
    public Transform Player;
    public float posx;
    public float negx;
    public float posy;
    public float negy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newpos, followspeed * Time.deltaTime);
        newpos.Normalize();
        if(transform.position.x > posx)
        {
            transform.position = new Vector3(posx, transform.position.y,-10);
        }
        else if(transform.position.x < negx)
        {
            transform.position = new Vector3(negx, transform.position.y,-10);
        }
        else if(transform.position.y > posy)
        {
            transform.position = new Vector3(transform.position.x, posy,-10);
        }
        else if(transform.position.y < negy)
        {
            transform.position = new Vector3(transform.position.x, negy,-10);
        }
    }
}
