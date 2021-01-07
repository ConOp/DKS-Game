using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager
{
    #region Singleton
    private static UIManager instance = null;
    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            return new UIManager();
        }
        return instance;
    }
    private UIManager()
    {
        LoadUI();
    }
    #endregion

    List<GameObject> UIList;

    bool LoadUI()
    {
        try
        {
            UIList = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/PlayerUI/"));
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Searches for object with specific name in UIList
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    GameObject SearchObject(string name)
    {
        if (UIList.Exists((GameObject x) => x.name == name))
        {
            foreach (GameObject ui in UIList)
            {
                if (ui.name == name)
                {
                    return ui;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// returns a reference to the TargetLock object.
    /// </summary>
    /// <returns></returns>
    public GameObject TargetLockObject()
    {
        return SearchObject("TargetLock");        
    }

    /// <summary>
    ///  returns a reference to the PopUpButton object, with specific text and OnClick method.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public GameObject PopUpButtonObject()
    {
        return SearchObject("PopUpButton");
    }

    void Test()
    {
        Debug.LogError("test");
    }
}
