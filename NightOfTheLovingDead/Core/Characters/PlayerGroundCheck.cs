using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
	[SerializeField]
	private PlayerMovement _playerController;

	void Awake()
	{
		this._playerController = GetComponentInParent<PlayerMovement>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == this._playerController.gameObject)
			return;

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == this._playerController.gameObject)
			return;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == this._playerController.gameObject)
			return;

	}
}
