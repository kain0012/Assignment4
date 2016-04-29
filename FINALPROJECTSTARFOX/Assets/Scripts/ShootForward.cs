using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel
/// Used to shoot bullets from the ships twin blasters.
/// </summary>
public class ShootForward : MonoBehaviour
{
	public Rigidbody bullet;
	public float velocity = 10.0f;

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Rigidbody newBullet = Instantiate(bullet,transform.position,transform.rotation) as Rigidbody;
			newBullet.AddForce(transform.forward*velocity,ForceMode.VelocityChange);
		}
	}
}
