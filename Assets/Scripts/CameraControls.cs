using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
	public Transform player;
	public bool isLockedToPlayer = false;
	public float facingOffset = 3.5f;
	public bool isFacingLeft = false;
	public float currentFacingOffset;
	public float xOffset = 0f;
	public float yOffset = 0f;
	public float zOffset = -10f;
	public float maximumXOffset = 7f;
	public float maximumYOffset = 5f;
	public Vector3 fullOffset;
	public float cameraTravelSpeed = 5f;

	public float mouseMass = 1f; //set to 0 to remove mouse tracking
	private GameObject mouse;
	public Vector2 worldMousePos;

	public List<GameObject> ObjectsWithCameraMass;
	public float cameraMassRadius = 15f;

    // Start is called before the first frame update
    void Start()
    {
		if(mouseMass != 0)
		{
			mouse = new GameObject("Mouse");
			AddObjectWithCameraMass(mouse);
		}
		
		fullOffset = new Vector3(xOffset, yOffset, zOffset);
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
		UpdateOffset();

		if (isLockedToPlayer)
		{
			transform.position = player.position + new Vector3(0,0,zOffset);
		}
		else //interpolate towards the player or offset
		{
			transform.position = Vector3.Lerp(transform.position, player.position + fullOffset, Time.deltaTime * cameraTravelSpeed);
		}
    }

	void UpdateOffset()
	{
		UpdateFacingOffset();
		
		if(mouseMass != 0)
		{
			worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouse.transform.position = new Vector3(worldMousePos.x, worldMousePos.y, 0);
		}

		Vector2 tempVector2Offset = new Vector2(0, 0);

		foreach(GameObject obj in ObjectsWithCameraMass)
		{
			float tempXOffset = obj.transform.position.x - transform.position.x;
			float tempYOffset = obj.transform.position.y - transform.position.y;
			float distance = Mathf.Sqrt(tempXOffset * tempXOffset + tempYOffset * tempYOffset);
			if(distance < cameraMassRadius)
			{
				tempVector2Offset += new Vector2(tempXOffset, tempYOffset);
			}			
		}


		xOffset = tempVector2Offset.x + currentFacingOffset;
		yOffset = tempVector2Offset.y;
		tempVector2Offset /= ObjectsWithCameraMass.Count + 1;

		

		if (xOffset > maximumXOffset)
		{
			xOffset = maximumXOffset;
		}
		else if (xOffset < maximumXOffset * -1)
		{
			xOffset = maximumXOffset * -1;
		}

		if (yOffset > maximumYOffset)
		{
			yOffset = maximumYOffset;
		}
		else if (yOffset < maximumYOffset * -1)
		{
			yOffset = maximumYOffset * -1;
		}

		fullOffset = new Vector3(xOffset, yOffset, zOffset);
	}

	public void UpdateFacingOffset()
	{
		if (isFacingLeft)
		{
			currentFacingOffset = facingOffset * -1;
		}
		else
		{
			currentFacingOffset = facingOffset;
		}
	}

	public void AddObjectWithCameraMass(GameObject obj)
	{
		ObjectsWithCameraMass.Add(obj);
	}
	public void RemoveObjectWithCameraMass(GameObject obj)
	{
		ObjectsWithCameraMass.Remove(obj);
	}
}
