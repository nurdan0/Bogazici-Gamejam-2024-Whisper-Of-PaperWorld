using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controller : MonoBehaviour
{
    public GameObject FireIvyPrefab;
    private bool isFired = true;
    public float health;
    public Slider healthBar;
    public float damageAmount;
    public float enemyattackspeed;
    private PlayerController playerController;
    public ParticleSystem enemyparticle;
    public AudioSource hitsoundenemy;
    

    private Shake_Manager shakeManager;

    void Start()
    {
        health = 100f;
        damageAmount = 10f;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shakeManager = GameObject.FindGameObjectWithTag("SHAKEIT").GetComponent<Shake_Manager>();
        FireIvyPrefab = Instantiate(FireIvyPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        healthBar.value = health;
        FireIvyPrefab.transform.Translate(-enemyattackspeed * Time.deltaTime * 35f, 0, 0);
        if (isFired)
        {
            StartCoroutine(Move());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("STAR"))
        {
            hitsoundenemy.Play();
            shakeManager.CamShake();
            TakeDamage(damageAmount);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemyparticle.Play();
            playerController.StartCoroutine(playerController.Mans(5));
            Destroy(FireIvyPrefab);
            Destroy(gameObject);
        }
    }


    private IEnumerator Move()
    {
        isFired = false;
        yield return new WaitForSeconds(4f);
        isFired = true;
        if (playerController.isLoop == false)
        {
            FireIvyPrefab.SetActive(true);
            FireIvyPrefab.transform.position = this.transform.position;
        }
    }
}
