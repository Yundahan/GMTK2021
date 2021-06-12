using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
	const float movementCD = 0.5f;
	const float v = 2f;//v * movementCD = 1
	
	Vector2Int intPos = new Vector2Int(0, 0);
	Vector2Int resetPos;
	
	float lastMovement;
	Vector3 velocity;
	
    // Start is called before the first frame update
    void Start()
    {
		lastMovement = -movementCD;
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time >= movementCD + lastMovement)//move only if last move is finished
		{
			if(velocity.x != 0f || velocity.y != 0f)
			{
				velocity = new Vector3(0f, 0f, 0f);
				transform.position = new Vector3((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y), 0f);//move was finished, round position to exactly match tiling
				intPos = new Vector2Int((int)(transform.position.x + 0.5f), (int)(transform.position.y + 0.5f));
			}
			
			if(Input.GetKey(KeyCode.UpArrow))
			{
				velocity = new Vector3(0f, v, 0f);
				lastMovement = Time.time;
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				velocity = new Vector3(0f, -v, 0f);
				lastMovement = Time.time;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				velocity = new Vector3(v, 0f, 0f);
				lastMovement = Time.time;
			}
			else if(Input.GetKey(KeyCode.LeftArrow))
			{
				velocity = new Vector3(-v, 0f, 0f);
				lastMovement = Time.time;
			}
		}
		
		transform.position += Time.deltaTime * velocity;//positional update
    }
	
	public bool IsMoving()
	{
		if(velocity.x != 0f || velocity.y != 0f)
		{
			return true;
		}
		
		return false;
	}
	
	public Vector2 GetIntPos()
	{
		return intPos;
	}
	
	public void UpdateData()
	{
		resetPos = intPos;
	}
	
	public void Reset()
	{
		velocity = new Vector3(0f, 0f, 0f);
		intPos = resetPos;
		transform.position = new Vector3(intPos.x, intPos.y, 0f);
	}
}
