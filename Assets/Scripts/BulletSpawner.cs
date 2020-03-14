using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public GameObject bulletPrefab;
	private float fireTimer = 0f;
	private float fireTimerMax = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		fireTimer += Time.deltaTime;
		if (fireTimer >= fireTimerMax)
		{
			Instantiate(bulletPrefab, transform);
			fireTimer = 0;
		}
    }
}
