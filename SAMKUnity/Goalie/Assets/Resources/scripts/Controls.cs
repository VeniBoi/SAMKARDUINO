using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{


    // Forum code warning

    static float zdepth = 1;
    public int part;
    public Vector3 m_position;

    void Update()
    {
        // Retrieve mouse position
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        m_position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, zdepth));
        //transform.position = new Vector3(transform.position.x, m_position.y, zdepth);
        Quaternion rot = Quaternion.LookRotation(transform.position - m_position, transform.forward);

        if (part == 0 && Input.GetMouseButton(0))
        {
            gameObject.transform.rotation = rot;
            gameObject.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }

        if (part == 1 && Input.GetMouseButton(1))
        {
            gameObject.transform.rotation = rot;
            gameObject.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }

        if (part == 2 && Input.GetMouseButton(2))
        {
            gameObject.transform.rotation = rot;
            gameObject.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }

    }
}
