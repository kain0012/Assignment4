using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from Pushypixel.
/// This is used by the HealthManager gameobject for each level.
/// It informs the player of his health, when he should explode and subsequently
/// when he should respawn.
/// </summary>
public class HealthManager : MonoBehaviour
{
	private static HealthManager instance = null;
	public static HealthManager Instance
	{
		get { return instance; }
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		gameObject.name = "$HealthManager";
	}
	
	public string healthDisplayTag = "HealthDisplay";
	private Transform healthDisplay;
	public GameObject damageEffect;
	public float maxHealth = 10.0f;
	public float damageCooldown = 1.0f;
	private float damageCooldownTimer = 0.0f;
	private float currentHealth;
	private float healthOriginalYScale;
	public Color maxHealthColor = Color.green;
	public Color minHealthColor = Color.red;
	public int initialLives = 3;
	private int currentLives;
	public GameObject playerDeathEffect;
	private GameObject player;
	public GameObject playerDamageSound;
	public GameObject playerDeathSound;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		currentHealth = maxHealth;
		currentLives = initialLives;
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthOriginalYScale = healthDisplay.localScale.y;
	}
	
	/*
	void OnLevelWasLoaded(int level)
	{
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		Debug.Log(healthDisplay.localScale);
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYScale*(currentHealth/maxHealth),healthDisplay.localScale.z);
		healthDisplay.GetComponentInChildren<Renderer>().material.color = Color.Lerp(minHealthColor,maxHealthColor,currentHealth/maxHealth);
		Debug.Log(healthDisplay.localScale);
	}
	*/
	
	public void DamagePlayer(float damageAmount)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if(damageCooldownTimer <= 0)
		{
			if(damageAmount < 0)
			{
				Debug.LogError("You cannot pass a negative value to DamagePlayer!  Please use RestoreHealth instead!");
				return;
			}
			currentHealth -= damageAmount;
			healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
			if(currentHealth <= 0.0f)
			{
				Instantiate(playerDeathSound,player.transform.position,player.transform.rotation);
				currentHealth = 0;
				healthDisplay.GetComponentInChildren<Renderer>().enabled = false;
				//Gameoverlogic here (or at least lose a life)
				player.SendMessageUpwards("SetDead",true);
				player.GetComponent<Rigidbody>().isKinematic = false;
				player.GetComponent<Rigidbody>().AddForce(player.transform.forward*10.0f,ForceMode.VelocityChange);
				//player.rigidbody.AddForce(Transform.forward*10.0f,ForceMode.VelocityChange);
				Invoke("Explode",1.0f);
			}
			
			Instantiate(playerDamageSound,player.transform.position,player.transform.rotation);
			healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYScale*(currentHealth/maxHealth),healthDisplay.localScale.x);
			healthDisplay.GetComponentInChildren<Renderer>().material.color = Color.Lerp(minHealthColor,maxHealthColor,currentHealth/maxHealth);
			Instantiate(damageEffect,player.transform.position,Quaternion.identity);
			damageCooldownTimer = damageCooldown;
		}
	}
	
	void Explode()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Instantiate(playerDeathEffect, player.transform.position, Quaternion.identity);
		player.SendMessageUpwards("ResetPosition");
		player.SendMessageUpwards("ResetRotation");
		player.GetComponent<Rigidbody>().isKinematic = true;
		player.SetActive(false);
		Invoke("RespawnPlayer",2.0f);
	}
	
	void RespawnPlayer()
	{
		currentLives--;
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.Euler(0,90,0);
		player.SetActive(true);
		ResetHealth();
		player.SendMessageUpwards("SetDead",false);
		//Check to see if we are out of lives at some point
	}
	
	public void RestoreHealth(float healthAmaount)
	{
		if(healthAmaount < 0)
		{
			Debug.LogError("You cannot pass a negative value to RestoreHealth!  Please use DamagePlayer instead!");
			return;
		}
		currentHealth += healthAmaount;
		if(currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYScale*(currentHealth/maxHealth),healthDisplay.localScale.x);
		healthDisplay.GetComponentInChildren<Renderer>().material.color = Color.Lerp(minHealthColor,maxHealthColor,currentHealth/maxHealth);
	}
	
	public void ResetHealth()
	{
		currentHealth = maxHealth;
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthDisplay.GetComponentInChildren<Renderer>().enabled = true;
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYScale,healthDisplay.localScale.x);
		healthDisplay.GetComponentInChildren<Renderer>().material.color = maxHealthColor;
	}
	
	void Update()
	{
		damageCooldownTimer -= Time.deltaTime;
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYScale*(currentHealth/maxHealth),healthDisplay.localScale.z);
		healthDisplay.GetComponentInChildren<Renderer>().material.color = Color.Lerp(minHealthColor,maxHealthColor,currentHealth/maxHealth);
	}
	
	void OnGUI()
	{
		GUILayout.Label("");
		GUILayout.Label("Lives: " + currentLives);
	}
}
