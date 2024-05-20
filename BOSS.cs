using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BOSS : MonoBehaviour
{
    public GameObject Wave;
    public GameObject Drops;
    public bool isAttack;
    public GameObject[] Spawners = new GameObject[3];
    public Animator anim;
    public Slider slider;
    public Slider easeslider;
    public int health;
    public Shake_Manager shakeManager;
    private PlayerController playerController;

    public AudioSource hitsoundenemy;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shakeManager = GameObject.FindGameObjectWithTag("SHAKEIT").GetComponent<Shake_Manager>();
        health = 5000;
        isAttack = false;
    }

    void Update()
    {
        if (slider.value != health)
            slider.value = health;
        if (slider.value != easeslider.value)
            easeslider.value = Mathf.Lerp(easeslider.value, health, 0.01f);
        if (isAttack == false)
        {
            StartCoroutine(WaitForTheFirstTime());
            isAttack = true;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "STAR")
        {
            hitsoundenemy.Play();
            health -= 20;
            if (health <= 0)
            {
                SceneManager.LoadScene(8);
                slider.value = 0;
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator WaitForTheFirstTime()
    {
        yield return new WaitForSeconds(0);
        StartCoroutine(Decisions());
    }

    IEnumerator Decisions()
    {
        yield return new WaitForSeconds(5);
        int random = Random.RandomRange(0, 2);
        if (random == 0)
        {
            playerController.StartCoroutine(playerController.Mans(2));
            anim.SetTrigger("jump");
            StartCoroutine(WaitWave());
            isAttack = false;
        }
        else if (random == 1)
        {
            playerController.StartCoroutine(playerController.Mans(2));
            anim.SetTrigger("drop");
            StartCoroutine(WaitDrops());
            isAttack = false;
        }
    }

    IEnumerator WaitWave()
    {
        yield return new WaitForSeconds(2.2f);
        shakeManager.CamShake();
        Instantiate(Wave, new Vector3(this.transform.position.x, this.transform.position.y - 3f, 0), Quaternion.identity);
    }

    IEnumerator WaitDrops()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            int rndm = Random.RandomRange(0, 3);
            switch (rndm)
            {
                case 0:
                    Instantiate(Drops, new Vector3(Spawners[0].transform.position.x, Spawners[0].transform.position.y, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Drops, new Vector3(Spawners[1].transform.position.x, Spawners[1].transform.position.y, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Drops, new Vector3(Spawners[2].transform.position.x, Spawners[2].transform.position.y, 0), Quaternion.identity);
                    break;
            }
        }
        
    }
}
