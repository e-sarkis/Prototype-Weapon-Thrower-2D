using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct representing Sprite and ID for use in simplistic animation
/// </summary>
[System.Serializable]
public struct SpriteMap
{
	public SpriteAnimator.SpriteAnimations ID;
	public Sprite sprite;
}

/// <summary>
/// Simplisitic prototyping Animator
/// </summary>
public class SpriteAnimator : MonoBehaviour 
{
	public enum SpriteAnimations { Idle, Move, Attack };
	public SpriteMap[] sprites;
	public Dictionary<SpriteAnimator.SpriteAnimations, Sprite> idsToSprites
		= new Dictionary<SpriteAnimator.SpriteAnimations, Sprite>();

	private SpriteRenderer _spriteRenderer;

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		// Populate dictionary
		foreach (SpriteMap sprMap in sprites)
		{
			idsToSprites.Add(sprMap.ID, sprMap.sprite);
		}
	}

	/// <summary>
	/// Change SpriteRenderer Sprite to given ID match if one exists
	/// </summary>
	/// <param name="ID">The SpriteAnimator.SpriteAnimations corresponding to the desired Sprite</param>
	public void changeSprite(SpriteAnimator.SpriteAnimations ID)
	{
		if (idsToSprites.ContainsKey(ID))
		{
			_spriteRenderer.sprite = idsToSprites[ID];
		} else
		{
			Debug.Log("Sprite with ID " + ID.ToString() + " could not be found!");
		}
	}
}
