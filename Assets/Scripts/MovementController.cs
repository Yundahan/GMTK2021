using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public TileMovement playerS;
	public TileMovement playerW;
	
	//scene constants
	Vector2Int offset = new Vector2Int(21, 0);
	const float movementCD = 0.5f;
	
	float lastMovement;
	bool connected = true;
	
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
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time >= movementCD + lastMovement)//move only if last move is finished
		{
			if(playerS.IsMoving())
			{
				playerS.FinishLastMove();
			}
			if(playerW.IsMoving())
			{
				playerW.FinishLastMove();
			}
		
			if(!connected)
			{
				if(ArePositionsEqual())
				{
					connected = true;
					playerS.SetRooted(false);
					playerW.SetRooted(false);
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
		if(!playerS.CanMoveInDirection(direction) && !playerW.CanMoveInDirection(direction))//both cannot move
		{
			playerS.TurnInDirection(direction);
			playerW.TurnInDirection(direction);
			return false;
		}
		
		if(playerS.CanMoveInDirection(direction) && playerW.CanMoveInDirection(direction))//both can move
		{
			playerS.Move(direction);
			playerW.Move(direction);
			lastMovement = Time.time;
			return true;
		}
		
		if(playerS.CanMoveInDirection(direction) && !playerW.CanMoveInDirection(direction))//only S can move
		{
			connected = false;
			playerS.Move(direction);
			playerW.SetRooted(true);
			lastMovement = Time.time;
		}
		
		if(!playerS.CanMoveInDirection(direction) && playerW.CanMoveInDirection(direction))//only W can move
		{
			connected = false;
			playerW.Move(direction);
			playerS.SetRooted(true);
			lastMovement = Time.time;
		}
		
		return false;
	}
	
	bool ArePositionsEqual()
	{
		if(playerS.GetIntPos().x + offset.x == playerW.GetIntPos().x && playerS.GetIntPos().y + offset.y == playerW.GetIntPos().y)
		{
			return true;
		}
		
		return false;
	}
}
