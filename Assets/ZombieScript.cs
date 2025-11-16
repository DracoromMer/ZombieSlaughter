using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody2D RigidB;
    public Animator anim;

    public float health = 1f;
    private float distance;
    
    public float distanceRange;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    private void FixedUpdate()
    {

    }

 void Animate()
    {
        //anim.SetFloat("Speedx", Directionx);
        //anim.SetFloat("Speedy", Directiony);

    }
    private void Chase()
    {
       distance = Vector2.Distance(transform.position, target.position);
        if (distance <= distanceRange)
        {
                Vector2 direction = (target.position - transform.position).normalized;
                RigidB.velocity = direction * moveSpeed;
            }
            else
            {
                RigidB.velocity = Vector2.zero; // Stop moving if player is out of range
            }

    }

    public void zombieDeath(float dano)
    {
        health = health - dano;
        if (health<= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
    if (collision.gameObject.CompareTag("Player"))
            {
            collision.gameObject.GetComponent<PlayerScript>().takeDamage(10);
        }
    }

}
