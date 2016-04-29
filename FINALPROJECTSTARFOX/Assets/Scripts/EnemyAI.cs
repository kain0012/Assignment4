using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// This script is used from controlling the baddies in level 2.
/// </summary>
public class EnemyAI : MonoBehaviour {
	
	public float targetDistance = 10.0f;
	public float enemySpeed = 0.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	
	
	// Update is called once per frame
	void LateUpdate ()
	{
		GameObject plane = GameObject.FindGameObjectWithTag("GameplayPlane");
		if(transform.position.z - plane.transform.position.z <= targetDistance)
		{
			Vector3 newPosition = transform.position;
			newPosition.z = plane.transform.position.z + targetDistance;
			transform.position = newPosition;
			
		}
		else
		{
			transform.position += transform.forward*enemySpeed*Time.deltaTime;
		}
	}
}
