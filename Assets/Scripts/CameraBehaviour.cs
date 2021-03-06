using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public InventoryHandler invh;
	public GameObject[] players = new GameObject[2];
	
	AudioHandler ah;
	
	int currentPlayer = 0;//0:S, 1:W
	int resetCurrentPlayer = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        ah = (AudioHandler)(gameObject.GetComponent<AudioHandler>());
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = players[currentPlayer].transform.position + new Vector3(0f, 0f, -10f);
    }
	
	public int GetCurrentPlayer()
	{
		return currentPlayer;
	}
	
	public void SwitchCamera()
	{
		int tempPlayer = 1 - currentPlayer;
		TileMovement temp = (TileMovement)players[tempPlayer].GetComponent(typeof(TileMovement));
		
		if(temp.GetRooted())//dont switch camera if the player is rooted
		{
			return;
		}
		
		currentPlayer = 1 - currentPlayer;
		invh.SetCurrentPlayer(currentPlayer);
		ah.SwitchTracks();
	}
	
	public void UpdateData()
	{
		resetCurrentPlayer = currentPlayer;
	}
	
	public void Reset()
	{
		currentPlayer = resetCurrentPlayer;
		ah.Reset(currentPlayer);
	}
}
