using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupies : MonoBehaviour
{
	public List<Vector2> shifts = new List<Vector2>();
	List<Vector2Int> occupiedPositions = new List<Vector2Int>();
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public List<Vector2Int> Init()
	{
		foreach(Vector2 shift in shifts)
		{
			int shiftx = (int)Math.Round(transform.position.x + shift.x);
			int shifty = (int)Math.Round(transform.position.y + shift.y);
			occupiedPositions.Add(new Vector2Int(shiftx, shifty));
		}
		
		shifts.Clear();
		return occupiedPositions;
	}
	
	public List<Vector2Int> GetOccupiedPositions()
	{
		return occupiedPositions;
	}
}
