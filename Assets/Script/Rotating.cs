using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public Transform target;//the target object
    private float speedMod = 5.0f;//a speed modifier
    private Vector3 point;//the coord to the point where the camera looks at
    public float zoom = 80f;
    void Start()
    {//Set up things on the start method
        point = target.transform.position;//get target's coords
        transform.LookAt(point);//makes the camera look to it
    }

    void Update()
    {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0.0f, 0.2f, -0.5f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0.0f, -0.2f, 0.5f));
        }
    }
}
