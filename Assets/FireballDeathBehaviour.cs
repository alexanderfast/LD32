using UnityEngine;
using System.Collections;

public class FireballDeathBehaviour : MonoBehaviour {

	public AudioClip DeathSound;

	// Use this for initialization
	void Start ()
	{
		AudioSource.PlayClipAtPoint(DeathSound, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// called as animation event
	public void Destroy()
	{
		Destroy(gameObject);
	}
}
