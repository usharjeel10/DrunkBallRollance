using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (ControlFreak2.CF2Input.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            Application.LoadLevelAsync(Application.loadedLevelName);
        }
    }
}
