using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar_Controller : MonoBehaviour
{
    public float speed = -2f;

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.eulerAngles.z + speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BOSS"))
        {
            Destroy(this.gameObject);
        }
    }
}
