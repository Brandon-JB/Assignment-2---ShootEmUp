using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    #region Variables

    [Header ("Player")]

    public int speed;
    public GameObject player;
    public Rigidbody2D rb;
    private float horizontal;
    private float vertical;

    [Header("Gun")]

    public GameObject bullet;
    public Transform firepoint;
    public float firingCooldown;
    private float cooldownTimePassed;

    [Header("Pause Menu")]

    private bool isPaused = false;

    [Header("Win/Lose")]

    public float timeToSurvive;
    public float timeSurvived;


    #endregion

    private void Start()
    {
        isPaused = false;
        timeSurvived = 0;
    }
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && isPaused == false )
        {
            Shoot();
        }

        if (Time.deltaTime > 1)
        {
            isPaused = false;
        }
        else if (Time.deltaTime == 0)
        {
            isPaused = true;
        }

        TimePassed();

        if(timeSurvived >= timeToSurvive)
        {
            SceneManager.LoadScene("WinScreen");
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    #region Functions

    void Shoot()
    {
        if (cooldownTimePassed >= firingCooldown)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            cooldownTimePassed = 0;
        }
    }

    void TimePassed()
    {
        cooldownTimePassed += Time.deltaTime;
        timeSurvived += Time.deltaTime;
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    #endregion
}
