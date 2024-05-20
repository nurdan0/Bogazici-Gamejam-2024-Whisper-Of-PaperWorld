using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float groundCheckRadius;
    public GameObject projectilePrefab;
    public Transform throwPoint;
    private Animator animator;
    private Rigidbody2D rb;
    public Transform groundCheck;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float throwForce = 10f;
    public float loopCount = 0;
    public float maxSpam = 0;
    public float spamValue;

    public GameObject[] mans = new GameObject[6];
    public GameObject helpText;

    public Slider spamSlider;
    public GameObject spamObject;
    public GameObject rescueMan;
    public GameObject closed_Case;
    public GameObject opened_Case;

    private bool facingRight = true;
    public bool isLooping = false;
    public bool isIvy = false;
    private bool isGrounded;
    public bool isLoop = false;
    public bool canThrow = false;
    public bool hasKey = false;
    public bool getkey = false;
    public bool iscaseopen = false;

    public AudioSource hitsound;


    // Shield
    public GameObject ShieldobjectToSpawn;
    public Transform spawnPoint;
    public float spawnForce = 500f;
    public bool shieldget = false;
    public bool isProtected = false;
    public GameObject shield;
    

    public AudioSource ninjastarsound;
    public AudioSource anahtarkeysound;
    public RopeController ropeController;

    private Shake_Manager shakeManager;
    public GameObject DeadPlayer;

    public bool isDead = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            isProtected = false;
        }
        else
            isProtected = (PlayerPrefs.GetInt("isProtected") != 0);
        
        shakeManager = GameObject.FindGameObjectWithTag("SHAKEIT").GetComponent<Shake_Manager>();
        ShieldobjectToSpawn.SetActive(false);
        PlayerPrefs.GetInt("hasKey", hasKey ? 1 : 0);
        PlayerPrefs.GetInt("canThrow", canThrow ? 1 : 0);
        for (int i = 0; i < mans.Length; i++)
        {
            mans[i].SetActive(false);
        }
        mans[0].SetActive(true);
        spamObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spamValue = maxSpam;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("SHIELD"))
        {
            isProtected = true;
            PlayerPrefs.SetInt("isProtected", isProtected ? 1 : 0);
            shield.SetActive(true);
            ShieldobjectToSpawn.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DROPS"))
        {
            DeadPlayer.SetActive(true);
            shakeManager.CamShake();
            Destroy(collision.gameObject);
            StartCoroutine(WaitDead());
        }

        if (collision.gameObject.CompareTag("WAVE"))
        {
            if (isProtected == false || !facingRight)
            {
                DeadPlayer.SetActive(true);
                shakeManager.CamShake();
                StartCoroutine(Mans(4));
                StartCoroutine(WaitDead());
            }
            else
            {
                StartCoroutine(Mans(3));
                shakeManager.CamShake();
                Destroy(collision.gameObject);

            }
        }

        if (collision.gameObject.CompareTag("IVY"))
        {
            StartCoroutine(Mans(1));
            isGrounded = false;
            moveSpeed = 0.5f;
            isIvy = true;
        }

        if (collision.gameObject.CompareTag("FIREBALL"))
        {
            if (isProtected == false || !facingRight)
            {
                shakeManager.CamShake();
                StartCoroutine(Mans(4));
                hitsound.Play();
                isLoop = true;
            }
            else
            {
                StartCoroutine(Mans(3));
                shakeManager.CamShake();
                collision.gameObject.SetActive(false);

            }
            
        }

        if (collision.gameObject.CompareTag("KEY"))
        {
            anahtarkeysound.Play();
            ropeController.key.SetActive(false);
            getkey = true;
        }

        if (collision.gameObject.CompareTag("CASE"))
        {
            if (iscaseopen == false && getkey)
            {
                ShieldobjectToSpawn.SetActive(true);
                closed_Case.SetActive(false);
                opened_Case.SetActive(true);
                SpawnObject();
            }
        }
        
    }

    void SpawnObject()
    {
        if (rb != null && shieldget == false)
        {
            GameObject spawnedObject = Instantiate(ShieldobjectToSpawn, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, spawnForce));
            shieldget = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IVY"))
        {
            isGrounded = true;
            moveSpeed = 5f;
            isIvy = false;
        }
    }
    void Update()
    {
        DeadPlayer.transform.position = this.transform.position;
        spamSlider.transform.position = spamObject.transform.position;
        spamObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2, 0);
        if (spamSlider.value != spamValue)
        {
            spamSlider.value = spamValue;
        }

        if (isLoop)
        {
            spamObject.SetActive(true);
            spamValue = loopCount;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                loopCount += 5;
                
                if (loopCount >= 100)
                {
                    isLoop = false;
                    loopCount = 0;
                }
            }
            else if (!isLooping)
            {
                StartCoroutine(ReduceLoop());
            }
            moveSpeed = 0f;
        }
        else if (!isIvy)
        {
            spamObject.SetActive(false);
            moveSpeed = 5f;
        }
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;   
            }
        }

        if (animator != null)
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.E) && canThrow)
        {
            ninjastarsound.Play();
            ThrowNinjaStar();
        }
    }

    IEnumerator WaitDead()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ReduceLoop()
    {
        isLooping = true;
        if (loopCount >= 0)
            loopCount -= 1f;
        else if (loopCount <= 0)
            loopCount = 0;
        yield return new WaitForSeconds(0.1f);
        isLooping = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void ThrowNinjaStar()
    {
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        
        float direction = facingRight ? 1f : -1f;
        rbProjectile.velocity = new Vector2(direction * throwForce, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public IEnumerator Mans(int i)
    {
        for (int j = 0; j < mans.Length; j++)
        {
            if (j != i)
                mans[j].SetActive(false);
        }
        mans[i].SetActive(true);
        yield return new WaitForSeconds(2f);
        mans[i].SetActive(false);
        mans[0].SetActive(true);
    }
}
