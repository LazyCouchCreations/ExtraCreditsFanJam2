using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public GameObject bulletPrefab;
	private float fireTimer = 0f;
	private float fireTimerMax = 1f;
	public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire2"))
		{
			GameObject bullet = Instantiate(bulletPrefab, transform);
			bullet.GetComponent<Projectile>().Fire(target);
		}
		//fireTimer += Time.deltaTime;
		//if (fireTimer >= fireTimerMax)
		//{
		//	Instantiate(bulletPrefab, transform);
		//	fireTimer = 0;
		//}
    }
}
