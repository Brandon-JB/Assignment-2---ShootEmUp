using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //public Rigidbody2D rb;
    public GameObject bullet;
    //public int speed;

    private void Update()
    {
        //rb.velocity = transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(bullet);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(bullet);
        }
    }
}