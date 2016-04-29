using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// This script runs when the ship colides with either the final boss missles and lasers,
/// or when hitting the side of the level 1 arcs.
/// </summary>
public class DamagePlayerOnTrigger : MonoBehaviour
{
	public float damageAmount = 1.0f;
	public string collisionTag = "Player";
	public bool destroyOnTrigger = false;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == collisionTag)
		{
			HealthManager.Instance.DamagePlayer(damageAmount);
		}
		if(destroyOnTrigger)
		{
			Destroy (gameObject);
		}
	}
}
