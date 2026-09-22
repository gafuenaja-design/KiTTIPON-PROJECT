using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryUIManager : MonoBehaviour
{
  public static GameObject MilitaryUI;
    private void Awake()
    {
        MilitaryUI = gameObject;
        MilitaryUI.SetActive(false);
    }
}
