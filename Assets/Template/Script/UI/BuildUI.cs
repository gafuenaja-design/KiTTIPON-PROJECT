using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
   [SerializeField] private Button _buildbutton;
   [SerializeField] private GameObject _buildUI;

    void Awake()
    {
        _buildUI.SetActive(false);
    }
    public void onoffbuildUI()
    {
        bool _statenow = _buildUI.activeSelf;
        _buildUI.SetActive(!_statenow);
    }

    public void openbuildUI(bool on)
    {
        _buildUI.SetActive(on);
    }

}
