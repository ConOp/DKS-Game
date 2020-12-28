using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpButton
{
    GameObject canvas;

    /// <summary>
    /// Create a button based on a single button canvas. Requires the canvas holding the button, the text for the button, the method for onclick.
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="text"></param>
    /// <param name="method"></param>
    public PopUpButton(GameObject canvas, string text, UnityAction method)
    {
        this.canvas = GameObject.Instantiate(canvas);
        Button b = this.canvas.GetComponentInChildren<Button>();
        b.GetComponentInChildren<Text>().text = text;
        b.onClick.AddListener(method);
    }

    void Test()
    {
        Debug.LogError("Test");
    }

    public void HideButton()
    {
        canvas.SetActive(false);
    }

    public void ShowButton()
    {
        canvas.SetActive(true);
    }
}
