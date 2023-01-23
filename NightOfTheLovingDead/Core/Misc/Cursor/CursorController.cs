using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	public static CursorController Instance;

	private bool inGame;

    private void Start()
    {
		Instance = this;
		this.inGame = false;
		Cursor.visible = false;
	}

    public void LockCursor()
	{
		Cursor.visible = false;
		this.inGame = true;
		ResetMode();
	}

	public void UnlockCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		this.inGame = false;
	}

	private void ResetMode()
	{
		if (!this.inGame)
			return;
		Cursor.lockState = CursorLockMode.Locked; // фиксируем системный курсор по центру экрана
	}
}
