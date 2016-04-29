using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour
{
	public float minRotationChangeInterval = 1.0f;
	public float maxRotationChangeInterval = 5.0f;
	public float rotationSpeed = 30.0f;
	
	private Vector3 chosenRotation;

	// Use this for initialization
	void Start ()
	{
		chosenRotation = transform.rotation.eulerAngles;
		Invoke("RotationChange",Random.Range(minRotationChangeInterval,maxRotationChangeInterval));
	}
	
	void RotationChange()
	{
		chosenRotation = (Vector3)Random.insideUnitCircle;
		Debug.Log(chosenRotation);
		Invoke("RotationChange",Random.Range(minRotationChangeInterval,maxRotationChangeInterval));
	}
	
	void Update()
	{
		//transform.rotation = Quaternion.LookRotation(chosenRotation);
		//Debug.Log(transform.rotation.eulerAngles);
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,chosenRotation,rotationSpeed*Mathf.Deg2Rad*Time.deltaTime,0.0f));
	}
}
