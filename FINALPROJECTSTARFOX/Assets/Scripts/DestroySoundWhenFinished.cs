using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// Used when wanting to destroys a sound object when finished with explosions and whatnot.
/// </summary>
public class DestroySoundWhenFinished : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
	{
		if(!GetComponent<AudioSource>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
