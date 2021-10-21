using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
  public KeyCode screenShotKey = KeyCode.G;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(screenShotKey))
    {
      DateTime now = DateTime.UtcNow;
      string filename = $"Screenshots/ScreenShot {now:yyyy.MM.dd HHmm.ss.ff} utc.png";
      Debug.Log($"Saving screenshot to: {filename}");
      ScreenCapture.CaptureScreenshot(filename);
    }
  }
}