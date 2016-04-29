using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// Whenever we see an enemy die, We see an explosion of red.
/// The pretty sparkles must be destroyed. This does that.
/// </summary>
public class DestroyParticleWhenFinished : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
	{
		if(!GetComponent<ParticleSystem>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
