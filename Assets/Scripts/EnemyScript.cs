using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public enum enemyStates { WalkingL = 0, WalkingR = 1 };

    [Header("Movement")]
    public enemyStates state;
    public float flyingTime;
    public float speed;

    private CharacterController controller;
    private float timeFlown = 0f;

    [Header("Shooting")]

    public float shootCooldown;
    public GameObject bullet;
    public float cooldownTimePassed;
    public Transform firepoint;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case enemyStates.WalkingL:
                WalkingL();
                break;
            case enemyStates.WalkingR:
                WalkingR();
                break;
        }

        Shoot();
    }

    void WalkingL()
    {
        timeFlown += Time.deltaTime;

        controller.Move(Vector2.left * speed * Time.deltaTime);

        if (timeFlown >= flyingTime)
        {
            state = enemyStates.WalkingR;
            timeFlown = 0f;
        }
    }
    void WalkingR()
    {
        timeFlown += Time.deltaTime;

        controller.Move(Vector2.right * speed * Time.deltaTime);

        if (timeFlown >= flyingTime)
        {
            state = enemyStates.WalkingL;
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
}
