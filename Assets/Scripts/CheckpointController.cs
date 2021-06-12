using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
	public TileMovement playerS;
	public TileMovement playerW;
	
	Vector2[] cpPositionsS = {new Vector2(2f, 0f), new Vector2(2f, 1f), new Vector2(2f, 2f), new Vector2(2f, 3f)};
	Vector2[] cpPositionsW = {};
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.R))
		{
			Reset();
		}
		
        foreach(Vector2 cpPosition in cpPositionsS)
		{
			if(cpPosition == playerS.GetIntPos())
			{
				UpdateCheckpoint();
				return;
			}
		}
		
        foreach(Vector2 cpPosition in cpPositionsW)
		{
			if(cpPosition == playerW.GetIntPos())
			{
				UpdateCheckpoint();
				return;
			}
		}
    }
	
	void UpdateCheckpoint()
	{
		playerS.UpdateData();
		playerW.UpdateData();
	}
	
	void Reset()
	{
		playerS.Reset();
		playerW.Reset();
	}
}
