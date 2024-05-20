using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondshortvideo : MonoBehaviour
{
    public GameObject video;
    public GameObject Boss;
    public GameObject Bars;
    public Animator anim;

    IEnumerator Start()
    {
        Bars.SetActive(false);
        yield return new WaitForSeconds(9f);
        anim.SetTrigger("party");
        yield return new WaitForSeconds(1f);
        Bars.SetActive(true);
        video.SetActive(false);
    }

}
