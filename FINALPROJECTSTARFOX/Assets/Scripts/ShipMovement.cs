using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel.
/// This is a script that is used to manuever the ship on the x and y axis.
/// </summary>
public class ShipMovement : MonoBehaviour
{
	public Vector2 movementSpeed = Vector2.one;
	public float angleChangeSpeed = 50.0f;
	public bool isDead = false;
	
	void Start()
	{
		//Register for level changed events
		ScoreManager.Instance.LevelChanged += LevelChanged;
	}
	
	void OnDestroy()
	{
		ScoreManager.Instance.LevelChanged -= LevelChanged;
	}
	
	void OnDisable()
	{
		ScoreManager.Instance.LevelChanged -= LevelChanged;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isDead)
		{
			float horizontal = Input.GetAxis("Horizontal")*movementSpeed.x;
			float vertical = Input.GetAxis("Vertical")*movementSpeed.y;
			
			Vector3 direction = new Vector3(horizontal,vertical,0);
			Vector3 finalDirection = new Vector3(horizontal,vertical,1.0f);

            Vector3 possiblePosition = transform.position + direction * Time.deltaTime;

            if (possiblePosition.y > -4 && possiblePosition.y < 4 && possiblePosition.x < 9 && possiblePosition.x > -9)
            {
                transform.position += direction * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * angleChangeSpeed);
            }
		}
	}
	
	void SetDead(bool dead)
	{
		Debug.Log("I'm dead!");
		isDead = dead;
	}
	
	void ResetPosition()
	{
		transform.localPosition = Vector3.zero;
	}
	
	void LevelChanged(int newLevel)
	{
		Debug.Log ("ShipMovement: New Level Event Recieved! Level: " + newLevel);
	}
	
	void ResetRotation()
	{
		Debug.Log("Rotation resetting!");
		transform.localRotation = Quaternion.identity;
	}
}
