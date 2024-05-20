using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menureturn : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(18f);
        SceneManager.LoadScene(0);
    }
}
