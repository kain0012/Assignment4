using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed From PushyPixel
/// Everyone loves explosions! 
/// And this is why this script is used wherever
/// one might happen. Everything that is meant to die is blaze of glory
/// has this somewhere in it.
/// </summary>
public class GibOnTrigger : MonoBehaviour
{
	public string[] tags;
	public GameObject gib;
	public int pointValue = 100;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		foreach(string tag in tags)
		{
			if(col.tag == tag)
			{
				Instantiate(gib,transform.position,Random.rotation); 
				ScoreManager.Instance.score += pointValue;
				Destroy(gameObject);
			}
		}
	}
}
