using UnityEngine;
using System.Collections;
/// <summary>
/// Borrowed from pushypixel
/// Used to change the size of the height of the cylinder,
/// to create the illusion of closing distance to with during level 1.
/// </summary>
public class ScaleOverTime : MonoBehaviour
{
	public Vector3 goalScale = Vector3.one;
	public float time = 60.0f;
	private Vector3 initialScale;

	// Use this for initialization
	void Start ()
	{
		initialScale = transform.localScale;
		StartCoroutine(ScaleCoroutine());
	}
	
	IEnumerator ScaleCoroutine()
	{
		float t = 0.0f;
		
		while(t < time)
		{
			transform.localScale = Vector3.Lerp(initialScale, goalScale, t/time);
			t += Time.deltaTime;
			yield return null;
		}
		transform.localScale = goalScale;
	}
}
