using UnityEngine;
using System.Collections;
 /// <summary>
 /// Borrowed from pushy pixel
 /// used by MaterialFX gameobject to call HSBColor.cs to change "ship colour 2" found in the materials folder.
 /// This is used to make the bullets, and lasers change colour on the fly.
 /// </summary>
public class RandomMaterialHue : MonoBehaviour
{
	public Material[] materialsToChange;
	public float saturation = 1.0f;
	public float brightness = 1.0f;
	public float alpha = 1.0f;
	
	// Update is called once per frame
	void Update ()
	{
		HSBColor newColor = new HSBColor(Random.value,saturation,brightness,alpha);
		
		foreach(Material mat in materialsToChange)
		{	
			mat.color = newColor.ToColor();
		}
	}
}
