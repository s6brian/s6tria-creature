﻿/*
 * Brian Tria
 * July 15, 2015
 * 
 * HoverArea enables the hover effect for mech units only
 * 
 */

using UnityEngine;
using System.Collections;

public class HoverArea : MonoBehaviour 
{
	[SerializeField] private float p_fThrust = 15.0f;

	protected void OnTriggerStay (Collider p_collider)
	{
		if(p_collider.CompareTag("Mech"))
		{
			p_collider.attachedRigidbody.AddForce(Vector3.up * p_fThrust);
		}
	}
}
