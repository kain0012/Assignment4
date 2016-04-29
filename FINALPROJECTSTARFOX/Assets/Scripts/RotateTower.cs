using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel.
/// Used to create a rotation affect when the ship is on level 2.
/// </summary>
public class RotateTower : MonoBehaviour
{
	public float rotationSpeed = 10.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround(transform.forward, Input.GetAxis("Horizontal")*rotationSpeed*Mathf.Deg2Rad*Time.deltaTime);
	}
}
