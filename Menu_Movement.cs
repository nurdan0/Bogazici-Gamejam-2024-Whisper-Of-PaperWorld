using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Movement : MonoBehaviour
{
    public GameObject alex;
    public GameObject lily;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = Input.mousePosition;
        pos.Normalize();
        
        if (pos.x <= 0.8f)
            alex.transform.position = new Vector3(pos.x - 3 , alex.transform.position.y, alex.transform.position.z);
        else if (pos.x > 0.8f)
            alex.transform.position = new Vector3(pos.x - 3, alex.transform.position.y, alex.transform.position.z);

        if (pos.x <= 0.8f)
            lily.transform.position = new Vector3(pos.x + 2 , lily.transform.position.y, lily.transform.position.z);
        else if (pos.x > 0.8f)
            lily.transform.position = new Vector3(pos.x + 2, lily.transform.position.y, lily.transform.position.z);
    }
}
