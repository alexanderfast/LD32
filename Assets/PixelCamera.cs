using UnityEngine;
using System.Collections;

public class PixelCamera : MonoBehaviour {

	public float PixelsPerUnit = 100f;
	
	void Awake () {
		Camera.main.orthographicSize = (Screen.height / PixelsPerUnit / 2.0f);
	}
}
