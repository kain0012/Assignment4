using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from PushyPixel
/// The secret to this fight is that the big boss
/// has week spot in his turrets. This is where
/// you'll find this script.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
	public float maxHealth = 10.0f;
	private float currentHealth;
	public GameObject deathEffect;
	public GameObject damageEffect;
	public string collisionTag = "PlayerBullets";

	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == collisionTag)
		{
			currentHealth -= 1.0f; //Need to add to the bullets in some way
			Instantiate(damageEffect,col.ClosestPointOnBounds(transform.position),Quaternion.identity);
			Destroy(col.gameObject);
			
			if(currentHealth <= 0)
			{
				Instantiate(deathEffect,col.ClosestPointOnBounds(transform.position),Quaternion.identity);
				Destroy(gameObject);
			}
			
		}
	}
}
