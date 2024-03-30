using UnityEngine;
using System.Collections;

public class MouseLockCursor : MonoBehaviour {

	public bool isCursorLock = true;

	// Use this for initialization
	void Start () {
		LockCursor (isCursorLock);
	}
	
	// Update is called once per frame
	void Update() {
		if(ControlFreak2.CF2Input.GetButtonDown("Cancel")){
			LockCursor (false);
		}
		if(ControlFreak2.CF2Input.GetButtonDown("Fire1")){
			LockCursor (true);
		}
	}

	private void LockCursor(bool isLocked)
	{
		if (isLocked) 
		{
			ControlFreak2.CFCursor.visible = false;
			ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;
		} else {
			ControlFreak2.CFCursor.visible = true;
			ControlFreak2.CFCursor.lockState = CursorLockMode.None;
		}
	}
}
