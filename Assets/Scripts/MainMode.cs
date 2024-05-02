using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMode : MonoBehaviour
{
    void OnEnable()
    {
        UIController.ShowUI("Main");
    }

    public void OnPlaceObject() {
        ScreenLog.Log("Can't place objects in main mode.");
    }
}
