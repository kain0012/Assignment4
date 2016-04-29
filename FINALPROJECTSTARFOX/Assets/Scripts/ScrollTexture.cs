using UnityEngine;
using System.Collections;
/// <summary>
/// Used in level 1 and 3 to provide the illusion of movement,
/// by scrolling the texture of the GameplayPlane Plane.
/// </summary>
public class ScrollTexture : MonoBehaviour
{
	public Vector2 scrollSpeed = Vector2.one;
	private Material mat;

	// Use this for initialization
	void Start ()
	{
		mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		mat.mainTextureOffset += scrollSpeed*Time.deltaTime;
	}
}
