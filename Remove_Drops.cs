using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Drops : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4f);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
    }
}
