using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public TileMovement playerS;
	public TileMovement playerW;
	public SpriteRenderer torch;
	public InventoryHandler invh;
	public CameraBehaviour camerab;
	public Counter counter;
	public HowToDie displayD;
	
	//scene constants
	Vector2Int offset = new Vector2Int(0, 0);
	public float movementCD = 0.5f;
	List<Vector2Int> ops = new List<Vector2Int>();
	
	float lastMovement;
	bool connected = true;
	
	//base vectors
	Vector2Int upVector = new Vector2Int(0, 1);
	Vector2Int downVector = new Vector2Int(0, -1);
	Vector2Int rightVector = new Vector2Int(1, 0);
	Vector2Int leftVector = new Vector2Int(-1, 0);
	
    // Start is called before the first frame update
    void Start()
    {
		lastMovement = -movementCD;
        playerS.SetV(1f / movementCD);
        playerW.SetV(1f / movementCD);
		
		UpdateOps();//calculate occupied positions
		
		int offsetx = (int)(playerW.transform.position.x - playerS.transform.position.x);//calculate offset between players
		int offsety = (int)(playerW.transform.position.y - playerS.transform.position.y);
		offset = new Vector2Int(offsetx, offsety);
		
		torch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time >= movementCD + lastMovement)//move only if last move is finished
		{
			if(playerS.IsMoving() && playerW.IsMoving())
			{
				playerS.FinishLastMove();
				playerW.FinishLastMove();
				UpdateItems();
				displayD.DisplayDeathMessage();
			}
			else if(playerS.IsMoving())
			{
				playerS.FinishLastMove();
				UpdateItems();
				displayD.DisplayDeathMessage();
			}
			else if(playerW.IsMoving())
			{
				playerW.FinishLastMove();
				UpdateItems();
				displayD.DisplayDeathMessage();
			}
		
			if(!connected)
			{
				torch.enabled = true;
				
				if(ArePositionsEqual())
				{
					SetConnected(true);
					playerS.SetRooted(false);
					playerW.SetRooted(false);
					torch.enabled = false;
					counter.EndCounter();
				}
			}
			
			if(Input.GetKey(KeyCode.UpArrow))
			{
				Move(upVector);
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				Move(downVector);
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				Move(rightVector);
			}
			else if(Input.GetKey(KeyCode.LeftArrow))
			{
				Move(leftVector);
			}
		}
    }
	
	bool Move(Vector2Int direction)
	{
		if(!playerS.CanMoveInDirection(direction, ops) && !playerW.CanMoveInDirection(direction, ops))//both cannot move
		{
			playerS.TurnInDirection(direction);
			playerW.TurnInDirection(direction);
			return false;
		}
		
		if(playerS.CanMoveInDirection(direction, ops) && playerW.CanMoveInDirection(direction, ops))//both can move
		{
			playerS.Move(direction);
			playerW.Move(direction);
			lastMovement = Time.time;
			return true;
		}
		
		if(playerS.CanMoveInDirection(direction, ops) && !playerW.CanMoveInDirection(direction, ops))//only S can move
		{
			SetConnected(false);
			playerS.Move(direction);
			playerW.SetRooted(true);
			torch.transform.position = new Vector3(playerW.transform.position.x - offset.x, playerW.transform.position.y - offset.y, 0f);
			lastMovement = Time.time;
			camerab.SwitchCamera();
			counter.ActivateCounter();
		}
		
		if(!playerS.CanMoveInDirection(direction, ops) && playerW.CanMoveInDirection(direction, ops))//only W can move
		{
			SetConnected(false);
			playerW.Move(direction);
			playerS.SetRooted(true);
			torch.transform.position = new Vector3(playerS.transform.position.x + offset.x, playerS.transform.position.y + offset.y, 0f);
			lastMovement = Time.time;
			camerab.SwitchCamera();
			counter.ActivateCounter();
		}
		
		return false;
	}
	
	bool ArePositionsEqual()//are the players in the same position when account for offset?
	{
		if(playerS.GetIntPos().x + offset.x == playerW.GetIntPos().x && playerS.GetIntPos().y + offset.y == playerW.GetIntPos().y)
		{
			return true;
		}
		
		return false;
	}
	
	public void UpdateOps()//update list of occupied positions
	{
		ops.Clear();
		Occupies[] occupiers = (Occupies[])FindObjectsOfType(typeof(Occupies));
		
		foreach(Occupies occupier in occupiers)
		{
			ops.AddRange(occupier.Init());
		}
	}
	
	void UpdateItems()//check if a player stands on an item
	{
		Item[] items = (Item[])FindObjectsOfType(typeof(Item));
		
		foreach(Item item in items)
		{
			item.CheckPlayerPositions();
		}
	}
	
	public bool GetConnected()
	{
		return connected;
	}
	
	public void SetConnected(bool value)
	{
		connected = value;
		invh.SetConnected(value);
	}
}
