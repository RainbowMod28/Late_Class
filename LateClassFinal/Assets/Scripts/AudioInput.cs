using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
	AudioClip microphoneInput;
	bool microphoneInitialized;
	public float sensitivity;
	public float level;

	void Awake()
	{
		//init microphone input
		if (Microphone.devices.Length > 0)
		{
			microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
			microphoneInitialized = true;
		}
	}

	void Update() {

	}
	

	public void GetLoudness()
    {
		//get mic volume
		int dec = 128;
		float[] waveData = new float[dec];
		int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
		microphoneInput.GetData(waveData, micPosition);

		// Getting a peak on the last 128 samples
		float levelMax = 0;
		for (int i = 0; i < dec; i++)
		{
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak)
			{
				levelMax = wavePeak;
			}
		}
		level = Mathf.Sqrt(Mathf.Sqrt(levelMax));

		Debug.Log(level);
	}
}
