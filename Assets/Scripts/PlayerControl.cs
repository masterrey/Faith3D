using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator anim;
    public float speed = 10.0f;
    private Rigidbody rb;
    public GameObject target;
    public float distance = 5.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        
        anim.SetFloat("Speed", rb.velocity.magnitude*0.5f);
        if (direction.magnitude > distance)
        {
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            
        }
    }

    //when the player collides with the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //decrese he player's feith
            MenuGame.instance.DecreaseFaith(10);

        }
    }


}
