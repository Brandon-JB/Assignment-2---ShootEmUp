using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("Movement")]
    public float flyingTime;
    private float timeFlown = 0f;
    private float dirX;
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private Vector3 localScale;
    public GameObject Enemy;
    public bool movingLeft = true;

    [Header("Shooting")]

    public float shootCooldown;
    public GameObject bullet;
    public float cooldownTimePassed = 3f;
    public Transform firepoint;

    [Header("Active/Not")]
    private float timePassed = 0f;
    public bool currentlyActive = true;
    public float respawnTime = 0f;
    public float originalHeight;
    public AudioSource explode;



    

    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        if (movingLeft == true)
        {
            dirX = -1f;
        }

        else
        {
            dirX = 1f;
        }

        cooldownTimePassed = Random.Range(0f, 2.5f);
    }

    void Update()
    {
        timeFlown += Time.deltaTime;
        timePassed += Time.deltaTime;

        Shoot();

        if (timePassed >= respawnTime)
        {
            MoveIn();
        }

        TurnAround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            MoveOut();
            Debug.Log("beans");
        }

        if (collision.gameObject.tag == "KillAura")
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    #region Functions

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1f;

        transform.localScale = localScale;
    }

    void TurnAround()
    {
        timeFlown += Time.deltaTime;

        if (timeFlown >= flyingTime)
        {
            dirX *= -1f;
            timeFlown = 0f;
        }

    }

    void Shoot()
    {
        cooldownTimePassed += Time.deltaTime;

        if (cooldownTimePassed >= shootCooldown)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            cooldownTimePassed = 0;
        }
    }

    void MoveOut()
    {
            transform.position = new Vector3 (transform.position.x, 16.3f, transform.position.z);
            currentlyActive = false;
            timePassed = 0f;
        explode.Play();
    }

    void MoveIn()
    {
            transform.position = new Vector3(transform.position.x, originalHeight, transform.position.z);
            currentlyActive = true;
    }

    #endregion
}
