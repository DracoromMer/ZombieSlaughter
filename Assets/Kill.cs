using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("Bateu");
            collision.gameObject.GetComponent<ZombieScript>().zombieDeath(1);
        }
    Player = GameObject.FindGameObjectWithTag("Player");

    Player.GetComponent<PlayerScript>().Kill();
    }
}
