using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBUYUTUCU : MonoBehaviour
{
    Vector3 pos;
    public GameObject targetobject;
    private Vector3 originalScale;
    private float hoverScale = 0.8f;

    void Start()
    {
        originalScale = targetobject.transform.localScale;
    }

    void Update()
    {
        pos = Input.mousePosition;

        if(pos.x < 420 && pos.y > 900)
        {
            targetobject.transform.localScale = new Vector3(hoverScale, hoverScale, hoverScale);
        }
        else if(pos.x > 420 && pos.y < 900)
        {
            targetobject.transform.localScale = originalScale;
        }
    }
}
