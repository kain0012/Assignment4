using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// This Script ensures that the carmera follows the ship,
/// as it moves around the cylinder on that level.
/// Found under prefabs Gameplayplane->MainCamera.
/// </summary>
public class CameraFollowXY : MonoBehaviour
{
	public Transform objectToFollow;
	public Vector2 movementRatio = Vector2.one;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 newPosition = objectToFollow.position;
		newPosition.x *= movementRatio.x;
		newPosition.y *= movementRatio.y;
		newPosition.z = transform.position.z;
		transform.position = newPosition;
	}
}
