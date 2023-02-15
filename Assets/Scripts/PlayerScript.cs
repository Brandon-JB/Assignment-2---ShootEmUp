using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
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
    public AudioSource AudioSource;
    public AudioClip lasershot;

    [Header("Pause Menu")]

    private bool isPaused = false;
    public GameObject pauseMenu;

    [Header("Win/Lose")]

    public float timeToSurvive;
    public float timeSurvived;

    [Header("Hurt")]
    public float Health;
    public bool Vulnerable;
    private float timeInvulnerable;
    public float TimeToBeInvulnerable;
    public Animator animator;
    public string AnimationState = "Invulnerable";

    [Header("Health")]

    public GameObject Heart3;
    public GameObject Heart2;
    public GameObject Heart1;
    public AudioClip hurtSound;


    #endregion

    private void Start()
    {
        isPaused = false;
        timeSurvived = 0;
        Health = 3;
        Vulnerable = true;
        pauseMenu.SetActive(false);

        Heart3.SetActive(true);
        Heart2.SetActive(true);
        Heart1.SetActive(true);
    }
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && isPaused == false)
        {
            Shoot();
        }

        TimePassed();

        WinOrLose();

        InvulnerabilityTime();

        Pause();

        UpdateHealth();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && Vulnerable == true || collision.gameObject.tag == "EnemyBullet" && Vulnerable == true)
        {
            Health--;
            AudioSource.PlayOneShot(hurtSound, 1f);
            Vulnerable = false;
        }
    }

    #region Functions

    void Shoot()
    {
        if (cooldownTimePassed >= firingCooldown)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            cooldownTimePassed = 0;
            AudioSource.PlayOneShot(lasershot, 1f);
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

    void InvulnerabilityTime()
    {
        if (Vulnerable == false)
        {
            timeInvulnerable += Time.deltaTime;
            animator.SetBool(AnimationState, true);

        }

        if (timeInvulnerable >= TimeToBeInvulnerable)
        {
            Vulnerable = true;
            animator.SetBool(AnimationState, false);
            timeInvulnerable = 0f;
        }
    }

    void WinOrLose()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (timeSurvived >= timeToSurvive)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;

        }
    }

    void UpdateHealth()
    {
        if (Health == 2)
        {
            Heart3.SetActive(false);
        }

        if (Health == 1)
        {
            Heart2.SetActive(false);
        }

    }

    #endregion
}
