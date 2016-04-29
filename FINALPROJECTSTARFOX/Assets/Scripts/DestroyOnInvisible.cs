using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel
/// used to remove rings and archs when they are no longer visible.
/// </summary>
public class DestroyOnInvisible : MonoBehaviour
{
	public bool destroySelf = true;
	public GameObject[] destroyObjects;
	
	void OnBecameInvisible()
	{
		if(destroySelf)
		{
			Destroy(gameObject);
		}
		foreach(GameObject obj in destroyObjects)
		{
			Destroy(obj);
		}
	}
}
