using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeController : MonoBehaviour
{
    public GameObject rope;
    public Animator anim;
    public GameObject key;
    public ParticleSystem keyparticle;
    public GameObject keyimage;
    public PlayerController playerController;
    private Shake_Manager shakeManager;

    void Start()
    {
        key.SetActive(false);
        keyimage.SetActive(false);
        shakeManager = GameObject.FindGameObjectWithTag("SHAKEIT").GetComponent<Shake_Manager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("STAR"))
        {
            anim.enabled = false;
            StartCoroutine(KeyEnabledCoroutine());
        }
    }

    IEnumerator KeyEnabledCoroutine()
    {
       shakeManager.CamShake(); 
        rope.SetActive(false);
        yield return new WaitForSeconds(1);
        keyparticle.Play();
        key.SetActive(true);
    }

    void Update()
    {
        GetKey();
    }

    void GetKey()
    {
        if (playerController.getkey == true)
        {
            keyimage.SetActive(true);
        }
    }
}
