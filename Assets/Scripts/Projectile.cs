using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float velocity;
	public Rigidbody2D rb;
		
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player" && rb != null)
		{
			Destroy(collision.gameObject);
		}
		Destroy(rb);
	}

	public void Fire(Transform target)
	{
		Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, rotation);
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * velocity;
		rb.gravityScale = 0;
	}
}
