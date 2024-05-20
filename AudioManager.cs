using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(21);
        audioSource.Play();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
