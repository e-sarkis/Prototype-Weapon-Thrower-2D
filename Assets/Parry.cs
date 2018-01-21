using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour 
{
	void Start()
	{
		PlayerController parentPlayerController = GetComponentInParent<PlayerController>();
		if (parentPlayerController)
		{
			parentPlayerController.parryEvent += OnParry;
			// Note - for cooldowns Parry can unsubscribe and resubscribe
			// from parentPlayerController.parryEvent
		}
	}

	void OnParry()
	{
		// Attempt to Parry
	}
}
