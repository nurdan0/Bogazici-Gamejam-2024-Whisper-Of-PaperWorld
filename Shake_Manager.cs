using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Manager : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    public void CamShake()
    {
        anim.SetTrigger("Shake");
    }
}
