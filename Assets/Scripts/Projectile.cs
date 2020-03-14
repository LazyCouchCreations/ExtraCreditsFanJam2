using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject player;
	public float velocity;
	public Rigidbody2D rb;
	public bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, rotation);
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * velocity;
		rb.gravityScale = 0;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Destroy(collision.gameObject);
		}
		Destroy(rb);
	}
}
