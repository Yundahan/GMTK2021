using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public GameObject[] players = new GameObject[2];
	int currentPlayer = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))//switch camera from one player to the other
		{
			currentPlayer = 1 - currentPlayer;
		}
		
		transform.position = players[currentPlayer].transform.position + new Vector3(0f, 0f, -10f);
    }
}
