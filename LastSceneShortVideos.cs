using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneShortVideos : MonoBehaviour
{
    public GameObject video;
    public AudioManager firstaudio;

    IEnumerator Start()
    {
        firstaudio.audioSource.Stop();
        yield return new WaitForSeconds(24f);
        video.SetActive(false);
    }
}
