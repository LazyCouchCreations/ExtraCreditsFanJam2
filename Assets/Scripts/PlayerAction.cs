using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	public Transform target;
	public Vector3 mouse;
	public Transform centerOfMass;

	public Collider2D swingCollider;
	public float swingTimerMax;
	public float swingTimer = 0;

	public float reflectVelocity;
    
    // Update is called once per frame
    void Update()
    {

		//aim at the mouse
		mouse = Input.mousePosition;
		Vector3 mouse2d = Camera.main.ScreenToWorldPoint(mouse);
		mouse2d.z = 0;
		Debug.DrawLine(centerOfMass.position, mouse2d);

		//turn on/off swing collider
		if (Input.GetButtonDown("Fire1") && swingTimer == 0)
		{
			swingCollider.enabled = true;
		}

		if (swingCollider.enabled && swingTimer <= swingTimerMax)
		{
			swingTimer += Time.deltaTime;
		}
		else
		{
			swingTimer = 0;
			swingCollider.enabled = false;
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.gameObject.tag == "Projectile")
		{
			Vector2 direction = new Vector2(target.position.x - collision.transform.position.x, target.position.y - collision.transform.position.y);
			float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			collision.transform.eulerAngles = new Vector3(0, 0, rotation);

			collision.attachedRigidbody.velocity = collision.transform.right * reflectVelocity;
		}
	}
}
