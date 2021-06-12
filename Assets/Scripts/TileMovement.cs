using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
	float v;
	
	public Tilemap tilemap;
	
	String[] impassableTiles = {"wall", "water"};
	
	Vector2Int intPos = new Vector2Int(0, 0);
	Vector2Int resetPos;
	Vector2Int direction = new Vector2Int(1, 0);
	
	Vector3 velocity;
	
	bool rooted = false;
	
    // Start is called before the first frame update
    void Start()
    {
		intPos = new Vector2Int((int)(transform.position.x + 0.5f), (int)(transform.position.y + 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.E))
		{
			Vector3Int gridPos = new Vector3Int(intPos[0], intPos[1], 0);
			Debug.Log(gridPos);
			TileBase tb = tilemap.GetTile(gridPos);
			Debug.Log(tb.name);
		}
		
		transform.position += Time.deltaTime * velocity;//positional update
    }
	
	public bool CanMoveInDirection(Vector2Int direction)
	{
		if(rooted)
		{
			return false;
		}
		
		Vector3Int gridPos = new Vector3Int(intPos[0] + direction[0], intPos[1] + direction[1], 0);
		TileBase tb = tilemap.GetTile(gridPos);
		
		if(tb == null)//this should actually never trigger if wall detection and level layout are done correctly
		{
			return false;
		}
		
		foreach(String tile in impassableTiles)
		{
			if(tb.name == tile)
			{
				return false;
			}
		}
		
		return true;
	}
	
	public void Move(Vector2Int direction)
	{
		TurnInDirection(direction);
		velocity = new Vector3(v * direction.x, v * direction.y, 0f);
	}
	
	public bool IsMoving()
	{
		if(velocity.x != 0f || velocity.y != 0f)
		{
			return true;
		}
		
		return false;
	}
	
	public void FinishLastMove()
	{
		velocity = new Vector3(0f, 0f, 0f);
		intPos = new Vector2Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));
		transform.position = new Vector3((float)intPos.x, (float)intPos.y, 0f);//move was finished, round position to exactly match tiling
	}
	
	public Vector2Int GetIntPos()
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
	
	public void SetV(float value)
	{
		v = value;
	}
	
	public bool GetRooted()
	{
		return rooted;
	}
	
	public void SetRooted(bool value)
	{
		rooted = value;
	}
	
	public void TurnInDirection(Vector2Int value)
	{
		if(!rooted)
		{
			direction = value;
		}
	}
	
	public Vector2Int GetDirection()
	{
		return direction;
	}
}
