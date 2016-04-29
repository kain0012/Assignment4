using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Borrowed code for PUSHYPIXEL
//This Monster script controls the final boss.
//It basically does things in a loop with start() involving 
//4 different methods, each of which calls and IEnumerator that
//then calls the method that called it. Update just makes the boss face the player.
public class BigBossController : MonoBehaviour
{
	public Transform turretBase;
	public Transform cannonParent;
	public Transform[] lasers;
	public GameObject spawnLaser;
	public float laserRotationSpeed = 1.0f;
	public float laserShotDelay = 5.0f;
	public float laserChargeTime = 2.0f;
	public float laserSpeed = 1.0f;
	public float laserDuration = 1.0f;
	public GameObject laserChargeSound;
	public GameObject laserFireSound;
	private List<GameObject> laserChargeInstances = new List<GameObject>();
	private List<GameObject> laserFireInstances = new List<GameObject>();
	
	//public float movementSpeed = 1.0f;
	public float movementTime = 1.0f;
	public float movementRadius = 10.0f;
	public float movementDelay = 5.0f;
	private Vector3 originalPosition;
	private Vector3 goalPosition;
	private Vector3 currentPosition;
	
	public Transform[] missileSpawnPoints;
	public GameObject spawnMissile;
	public float missileDelay = 1.0f;
	
	private int missileIndex = 0;
	

	// Use this for initialization
	void Start ()
	{
		originalPosition = transform.position;
		Invoke("ChargeLaser", laserShotDelay);
		Invoke("ShootMissile",missileDelay);
		Invoke("MoveBoss",movementDelay);
	}
	
	void MoveBoss()
	{
		Vector3 movementVector = Random.insideUnitSphere*movementRadius;
		movementVector.y = 0.0f;
		goalPosition = originalPosition+movementVector;
		currentPosition = transform.position;
		StartCoroutine("ActuallyMoveBoss");
	}
	
	IEnumerator ActuallyMoveBoss()
	{
		float t = 0.0f;
		while(t < movementTime)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp(currentPosition,goalPosition,t/movementTime);
			yield return null;
		}
		Invoke("MoveBoss",movementDelay);
	}
	
	void ShootMissile()
	{
		Transform spawnPoint = missileSpawnPoints[missileIndex];
		missileIndex++;
		if(missileIndex >= missileSpawnPoints.Length)
		{
			missileIndex = 0;
		}
		GameObject instance = Instantiate(spawnMissile,spawnPoint.position,spawnPoint.rotation) as GameObject;
		instance.transform.parent = spawnPoint.transform.parent;
		Invoke("ShootMissile",missileDelay);
	}
	
	void ChargeLaser()
	{
		//Play some cool effect for charging, maybe some sounds, you know stuff like that.  Particles. Things.  ETC.
		foreach(Transform laser in lasers)
		{
			if(laser != null)
			{
				laser.gameObject.SetActive(true);
				laserChargeInstances.Add(Instantiate(laserChargeSound,laser.position,laser.rotation) as GameObject);
				
			}
		}
		
		Debug.Log("IMMA CHARGIN' MY LASER!");
		Invoke ("FireLaser",laserChargeTime);
	}
	
	void FireLaser()
	{
		foreach(GameObject soundObject in laserChargeInstances)
		{
			Destroy(soundObject);
		}
		laserChargeInstances.Clear();
		
		foreach(Transform laser in lasers)
		{
			laserFireInstances.Add(Instantiate(laserFireSound,laser.position,laser.rotation) as GameObject);
		}
			
		StartCoroutine("LaserCoroutine");
	}
	
	IEnumerator LaserCoroutine()
	{
		float t = 0.0f;
		while(t < laserDuration)
		{
			foreach(Transform laser in lasers)
			{
				if(laser != null)
				{
					Vector3 newScale = laser.localScale;
					newScale.z += laserSpeed*Time.deltaTime;
					laser.localScale = newScale;
				}
			}
			t += Time.deltaTime;
			yield return null;
		}
		LaserCooldown();
	}
	
	void LaserCooldown()
	{
		foreach(GameObject soundObject in laserFireInstances)
		{
			Destroy(soundObject);
		}
		laserFireInstances.Clear();
		
		//Reset lasers to initial values
		foreach(Transform laser in lasers)
		{
			if(laser != null)
			{
				Vector3 newScale = laser.localScale;
				newScale.z = 0;
				laser.localScale = newScale;
				laser.gameObject.SetActive(false);
				Instantiate(spawnLaser,laser.position,laser.rotation);
			}
		}
		Invoke("ChargeLaser", laserShotDelay);
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if(player != null)
		{
			//Creepily look at the player
			cannonParent.rotation = Quaternion.RotateTowards(cannonParent.rotation,Quaternion.LookRotation(player.transform.position - cannonParent.position),laserRotationSpeed*Time.deltaTime);
		}
	}
}
