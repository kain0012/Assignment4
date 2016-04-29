using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel
/// This is used to propell the ship and the entire plane forward on level 1 and 2.
/// </summary>
public class MoveForward : MonoBehaviour
{
	public float speed = 1.0f;

	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.forward*speed*Time.deltaTime;
	}
}
