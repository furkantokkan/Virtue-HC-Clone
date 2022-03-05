using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreensotOnNextFrame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        myCamera = gameObject.GetComponent<Camera>();
    }
    private void OnPostRender()
    {
        if (takeScreensotOnNextFrame)
        {
            takeScreensotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            GameObject level = LevelManager.Instance.GetCurrentLevelPrefab();
            string levelName = "DefaultLevel";
            if (level != null)
            {
                levelName = level.gameObject.name;
            }
             File.WriteAllBytes(Application.dataPath + "/Image" + " " + Application.productName + " " +
                Screen.width + "x" + Screen.height + " " + levelName + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + DateTime.Now.Millisecond +  ".png", byteArray);

            Debug.Log("Saved as: " + Application.dataPath + "/Image" + " " + Application.productName + " " +
                Screen.width + "x" + Screen.height + " " + levelName + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + DateTime.Now.Millisecond + ".png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }
    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreensotOnNextFrame = true;
    }

    public static void TakeScreenshotOnGame()
    {
        instance.TakeScreenshot(Screen.width, Screen.height);
    }
}
