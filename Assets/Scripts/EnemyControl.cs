using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Animator anim;
    public float speed = 10.0f;
    private Rigidbody rb;
    public GameObject target;
    public float distance = 5.0f;
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();

           target = other.gameObject;
        }
    }


}
