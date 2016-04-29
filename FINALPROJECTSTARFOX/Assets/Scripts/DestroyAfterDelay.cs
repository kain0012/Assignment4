using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// used to get rid of bullets after they have been fired.
/// </summary>
public class DestroyAfterDelay : MonoBehaviour
{
	public float delay = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Invoke("DestroyNow",delay);
	}
	
	void DestroyNow()
	{
		Destroy(gameObject);
	}
}
