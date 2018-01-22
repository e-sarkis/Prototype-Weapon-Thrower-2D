using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour 
{
	private PlayerController parentPlayerController;
	private SpriteRenderer _spriteRenderer;
	private Color _initialColor;
	private Color _invisibleColor = new Color(0f, 0f, 0f, 0f);
	private bool _parryActive = false;

	void Start()
	{
		parentPlayerController = GetComponentInParent<PlayerController>();
		if (parentPlayerController)
		{
			parentPlayerController.parryEvent += OnParry;
			//parentPlayerController.parryEndEvent += OnParryEnd; // Not currently in use
			// Note - for cooldowns Parry can unsubscribe and resubscribe
			// from parentPlayerController.parryEvent
		}
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_initialColor = _spriteRenderer.color;
		 _spriteRenderer.color = _invisibleColor;
	}

	void Update()
	{
		if (_parryActive)
		{
			parentPlayerController.timeSinceParryStart += Time.deltaTime;
			if (parentPlayerController.timeSinceParryStart > parentPlayerController.parryLength)
			{
				OnParryEnd();
			}
		}
	}

	void OnParry()
	{
		// Attempt to Parry
		_spriteRenderer.color = _initialColor;
		parentPlayerController.timeSinceParryStart = 0;
	}

	void OnParryEnd()
	{
		_spriteRenderer.color = _invisibleColor;
	}
}
