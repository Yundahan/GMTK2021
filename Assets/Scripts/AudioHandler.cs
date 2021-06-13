using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
	public AudioSource summerMusic;
	public AudioSource winterMusic;
	
	public float switchDuration = 1f;
	float lastSwitch;
	AudioSource current;
	
    // Start is called before the first frame update
    void Start()
    {
        lastSwitch = -switchDuration;
		current = summerMusic;
    }

    // Update is called once per frame
    void Update()
    {
		float timeSinceSwitch = Time.time - lastSwitch;
		
        if(timeSinceSwitch <= switchDuration)
		{
			
		}
    }
	
	public void SwitchTracks()
	{
		if(current == SummerMusic)
		{
			current = winterMusic;
		}
		else
		{
			current = summerMusic;
		}
	}
}
