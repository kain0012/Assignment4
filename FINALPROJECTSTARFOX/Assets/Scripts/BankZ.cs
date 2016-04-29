using UnityEngine;
using System.Collections;
//Borrowed code from PUSHYPIXEL
//Allows fr the ship to tilt sideways when pressing A and D or left and right.
//Can also be used to do a barrel roll when double tapping A or D.
//Checkout Hierarchy->GameplayPlane->Ship->Jet
public class BankZ : MonoBehaviour
{
	public float doubleTapDelay = 0.2f;
	public float barrelRollDuration = 1.0f;
	public float bankAxisAmount = 90.0f;
	public float movementAxisAmount = 25.0f;
	
	private float time = float.MaxValue;
	private bool buttonDown = false;
	private bool inBarrelRoll = false;
	private bool isDead;
	
	// Update is called once per frame
	void Update ()
	{
		if(!inBarrelRoll && !isDead)
		{
			float movementAxis = Input.GetAxis("Horizontal");
			float bankAxis = Input.GetAxis("Bank");
			
			if(Input.GetAxis("Bank") != 0)
			{
			
				//Single axis method (For windows/keyboard)
				Vector3 newRotationEuler = transform.rotation.eulerAngles;
				newRotationEuler.z = -bankAxisAmount*bankAxis;
				Quaternion newQuat = Quaternion.identity;
				newQuat.eulerAngles = newRotationEuler;
				transform.rotation = newQuat;
				
			}
			else
			{
				//Single axis method (For windows/keyboard)
				Vector3 newRotationEuler = transform.rotation.eulerAngles;
				newRotationEuler.z = -movementAxisAmount*movementAxis;
				Quaternion newQuat = Quaternion.identity;
				newQuat.eulerAngles = newRotationEuler;
				transform.rotation = newQuat;
			}
			
			//What we want:
			//We need a timer.  If you hit the button (Axis switches from 0 to non-0) the timer starts,
			//it resets when you go from 0 to non-zero again, and does a barrel roll if you do this within the time limit
			
			if(bankAxis == 0.0f)
			{
				buttonDown = false;
			}
			//We are not at 0!
			else if(buttonDown == false)
			{
				buttonDown = true;
				if(time < doubleTapDelay)
				{
					StartCoroutine("BarrelRollLeft");
				}
				time = 0.0f;
			}
			
			time += Time.deltaTime;
		}
	}
	
	void SetDead(bool dead)
	{
		Debug.Log("I'm dead!");
		isDead = dead;
	}
	
	IEnumerator BarrelRollLeft()
	{
		inBarrelRoll = true;
		float t = 0.0f;
		
		Vector3 initialRotation = transform.localRotation.eulerAngles;
		
		Vector3 goalRotation = initialRotation;
		goalRotation.z += 180.0f;
		
		Vector3 currentRotation = initialRotation;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.localRotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		t = 0;
		
		initialRotation = transform.localRotation.eulerAngles;
		goalRotation = initialRotation;
		goalRotation.z += 180.0f;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.localRotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		inBarrelRoll = false;
	}
	
	void ResetRotation()
	{
		Debug.Log("Rotation resetting!");
		transform.localRotation = Quaternion.identity;
	}
}
