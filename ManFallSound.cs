using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManFallSound : MonoBehaviour
{
    public AudioSource mansound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            mansound.Play();
        }
    }
}
