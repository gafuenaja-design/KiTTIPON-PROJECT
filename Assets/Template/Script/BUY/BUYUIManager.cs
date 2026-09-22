using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BUYUIManager : MonoBehaviour
{
  public static GameObject BUYUI;
    private void Awake()
    {
        BUYUI = gameObject;
        BUYUI.SetActive(false);
    }
}
