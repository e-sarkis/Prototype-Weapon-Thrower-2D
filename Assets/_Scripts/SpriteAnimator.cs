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

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		// Populate dictionary
		foreach (SpriteMap sprMap in sprites)
		{
			idsToSprites.Add(sprMap.ID, sprMap.sprite);
		}

		FindObjectOfType<PlayerMover>().moveEvent += OnMove;
		FindObjectOfType<PlayerMover>().moveDirectionEvent += OnMove;
		FindObjectOfType<PlayerMover>().stopEvent += OnStop;
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

	/// <summary>
	/// Change to Move Sprite
	/// </summary>
	private void OnMove()
	{
		changeSprite(SpriteAnimations.Move);
	}

	/// <summary>
	/// Change to Move Sprite and Correct Direction
	/// </summary>
	private void OnMove(float direction)
	{
		changeSprite(SpriteAnimations.Move);
		if (direction < 0)
		{
			_spriteRenderer.flipX = true;
		} else if (direction > 0)
		{
			_spriteRenderer.flipX = false;
		}
		
	}

	/// <summary>
	/// Change to Idle Sprite
	/// </summary>
	private void OnStop()
	{
		changeSprite(SpriteAnimations.Idle);
	}
}
