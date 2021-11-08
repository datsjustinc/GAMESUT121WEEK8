using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed; // player speed

    public float horizontal; // player direction left or right
    public float vertical; // player direction up or down

    public bool touching; // check if player is touching end green box
    
    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal"); 
        vertical = Input.GetAxis("Vertical");  

        Vector3 position = new Vector3(horizontal, vertical, 0f);
        transform.Translate(position * speed * Time.deltaTime); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Reset"))
         {
             touching = true;
         }

         if (collision.CompareTag("Loot"))
         {
             Destroy(collision.gameObject);
         }
    }

    void Start()
    {
        touching = false;
        //transform.position = new Vector3(-2.15f, - 3.13f, -11.71897f); // starting position
    }


    void Update()
    {
        Movement();
    }
}
