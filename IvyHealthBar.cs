using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IvyHealthBar : MonoBehaviour
{
    public float health;
    public Slider healthBar;
    public float damageAmount;
    public ParticleSystem particleSystem;
    private PlayerController playerController;

    private Shake_Manager shakeManager;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        health = 100f;
        damageAmount = 20f;
        shakeManager = GameObject.FindGameObjectWithTag("SHAKEIT").GetComponent<Shake_Manager>();
    }

    void Update()
    {
        healthBar.value = health / 100f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("STAR"))
        {
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
            playerController.StartCoroutine(playerController.Mans(5));
            particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(particleSystem, 1f);
            Destroy(gameObject);
        }
    }
}
