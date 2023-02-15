using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class SpearEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector3 localScale;
    public GameObject Enemy;
    public Transform playerPosition;
    public bool Activate;


    [Header("Time")]
    public float timeToWait;
    public float halfwayTime;
    private float timePassed;
    private float timeWaited;
    private float activationTimer;
    public float timeToActivate;

    [Header("Lunging")]
    public bool lunging;
    

    private void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

        Activate = false;
        lunging = false;

        timePassed = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= halfwayTime)
        {
            Activate = true;
        }

        if (Activate == true)
        {
            if (lunging == false)
            {
                activationTimer += Time.deltaTime;

                MoveToStrike();
            }
            else if (lunging == true)
            {
                timeWaited += Time.deltaTime;
                Lunge();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpearReset")
        {
            lunging = false;
            timeWaited = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }


    #region Functions

    void MoveToStrike()
    {
        if (activationTimer >= timeToActivate)
        {
            transform.position = new Vector3(playerPosition.transform.position.x, -13f, playerPosition.transform.position.z);
            activationTimer = 0f;
            lunging = true;
        }
    }

    void Lunge()
    {
        if (timeWaited >= timeToWait)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
    }

    #endregion
}
