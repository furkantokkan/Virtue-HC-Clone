using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraiseText3D : MonoBehaviour
{
    private TextMesh textMesh;
    private Transform mainCameraTransform;
    private UIManager uiManager;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        mainCameraTransform = Camera.main.transform;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
            mainCameraTransform.rotation * Vector3.up);
    }

    /// <summary>
    /// Karışık gelmesini istiyorsanız parametreyi boş "" yada null bırakın
    /// </summary>
    /// <param name="newText"></param>
    public void SetSettings(string newText)
    {
        if (String.IsNullOrEmpty(newText))
        {
            textMesh.text = uiManager.GetRandomWord();
        }
        else
        {
            textMesh.text = newText;
        }
    }
}
