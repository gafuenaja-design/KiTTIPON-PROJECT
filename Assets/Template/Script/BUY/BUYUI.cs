using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BUYUI : MonoBehaviour
{
    [SerializeField] private GameObject _BUYUI;
    [SerializeField] private GameObject _MilitaryBUYUI;

    void Start()
    {
        if(_BUYUI == null)
        {
            _BUYUI = BUYUIManager.BUYUI;
        }
        if(_MilitaryBUYUI == null)
        {
            _MilitaryBUYUI = MilitaryUIManager.MilitaryUI;
        }
    }

    private void OnMouseDown()
    {
        openBUY();
    }

    public void openBUY()
    {

        if (_BUYUI != null)
        {
            _BUYUI.SetActive(true);
            if (_MilitaryBUYUI != null)
            {
                _MilitaryBUYUI.SetActive(false);
            }
        }
        else
        {
            Debug.Log("i dont see");
        }
    }
}
