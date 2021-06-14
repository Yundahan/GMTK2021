using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
	public Tilemap tilemap;
	public InventoryHandler invh;
	
	public Sprite[] sprites = new Sprite[4];
	
	String[] impassableTiles = {"wall", "W Birke oben", "W Birke unten", "W Eiche oben Rechts", "W Eiche oben links", "W Eiche unten rechts", "W Eiche unten links", "W Steinchen", "W Buschi", "Birke oben", "Birke unten", "Eiche links oben", "Eiche links unten", "Eiche rechts oben", "Eiche rechts unten", "Steinchen", "Wasser", "Buschi", "Großer Stein", "W großer Stein", "W Hugellupf"};
	
	Vector2Int intPos = new Vector2Int(0, 0);
	Vector2Int resetPos;
	Vector2Int direction = new Vector2Int(1, 0);
	
	Vector3 velocity;
	
	bool rooted = false;
	float v;
	
	Animator animator;
	SpriteRenderer sr;
	
    // Start is called before the first frame update
    void Start()
    {
		animator = (Animator)gameObject.GetComponent<Animator>();
		sr = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		animator.enabled = false;
		intPos = new Vector2Int((int)Math.Round(transform.position.x ), (int)Math.Round(transform.position.y));
		UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += Time.deltaTime * velocity;//positional update
    }
	
	public bool CanMoveInDirection(Vector2Int direction, List<Vector2Int> ops)
	{
		if(rooted)
		{
			return false;
		}
		
		Vector3Int gridPos = new Vector3Int(intPos.x + direction.x, intPos.y + direction.y, 0);
		TileBase tb = tilemap.GetTile(gridPos);
		
		if(tb == null)//this should actually never trigger if wall detection and level layout are done correctly
		{
			return false;
		}
		
		foreach(String tile in impassableTiles)
		{
			if(tb.name == tile)
			{
				if(tb.name == "Wasser" && invh.IsItemInInventory(gameObject.name, "Floss"))
				{
					return true;
				}
				
				return false;
			}
		}
		
		foreach(Vector2Int position in ops)
		{
			if(position == new Vector2Int(gridPos.x, gridPos.y))
			{
				return false;
			}
		}
		
		return true;
	}
	
	public void Move(Vector2Int direction)
	{
		animator.enabled = true;
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
		animator.enabled = false;
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
		rooted = false;
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
			
			if(direction.x == 1)
			{
				sr.sprite = sprites[1];
				animator.SetInteger("dir", 0);
				return;
			}
			if(direction.x == -1)
			{
				sr.sprite = sprites[3];
				animator.SetInteger("dir", 1);
				return;
			}
			if(direction.y == 1)
			{
				sr.sprite = sprites[0];
				animator.SetInteger("dir", 3);
				return;
			}
			if(direction.y == -1)
			{
				sr.sprite = sprites[2];
				animator.SetInteger("dir", 2);
				return;
			}
		}
	}
	
	public Vector2Int GetDirection()
	{
		return direction;
	}
}
