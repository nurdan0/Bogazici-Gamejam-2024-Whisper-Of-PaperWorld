using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combine_Priceless : MonoBehaviour
{
    public GameObject Alex;
    public GameObject Lily;
    public bool isLooping;
    public float loopCount;
    public Slider slider;
    private Vector3 alpos;
    private Vector3 lipos;  

    // Start is called before the first frame update
    void Start()
    {
        loopCount = 0f;
        isLooping = false;
        alpos = Alex.transform.position;
        lipos = Lily.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value != loopCount)
            slider.value = loopCount;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            loopCount += 3.5f;
            Alex.transform.position = new Vector3(Alex.transform.position.x + 0.12f, alpos.y, 0);
            Lily.transform.position = new Vector3(Lily.transform.position.x - 0.12f, lipos.y, 0);
            if (loopCount >= 100)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else if (!isLooping)
        {
            
            StartCoroutine(Combine());
        }
    }

    IEnumerator Combine()
    {
        isLooping = true;
        if (loopCount >= 0)
            loopCount -= 1f;
        else if (loopCount <= 0)
            loopCount = 0;
        yield return new WaitForSeconds(0.1f);
        if (alpos.x < Alex.transform.position.x)
            Alex.transform.position = new Vector3(Alex.transform.position.x - 0.05f, alpos.y, 0);
        if (lipos.x > Lily.transform.position.x)
            Lily.transform.position = new Vector3(Lily.transform.position.x + 0.05f, lipos.y, 0);
        isLooping = false;
    }
}
