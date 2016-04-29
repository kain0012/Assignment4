using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// This script used to run when the ship colides with either the final boss missles and lasers,
/// or when hitting the side of the level 1 arcs, but was replaced with the DamagePlayerOnTrigger script.
/// </summary>
public class DamagePlayerOnCollision : MonoBehaviour
{
	public float damageAmount = 1.0f;
	public float coolDownTime = 1.0f;
	
	private bool inCooldown = false;
	
	void OnCollisionEnter()
	{
		if(!inCooldown)
		{
			HealthManager.Instance.DamagePlayer(damageAmount);
			inCooldown = true;
			Invoke("Uncool",coolDownTime);
		}
	}
	
	void Uncool()
	{
		inCooldown = false;
	}
}
