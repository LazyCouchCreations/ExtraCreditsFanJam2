using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	private Vector3 mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		mouse = Input.mousePosition;
		Vector3 mouse2d = Camera.main.ScreenToWorldPoint(mouse);
		mouse2d.z = 0;
		transform.position = mouse2d;
	}
}
