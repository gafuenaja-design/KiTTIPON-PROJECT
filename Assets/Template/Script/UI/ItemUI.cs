using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _woodUI;

   [SerializeField] private TextMeshProUGUI _stoneUI;

   [SerializeField] private TextMeshProUGUI _foodUI;

   [SerializeField] private TextMeshProUGUI _NPCUI;

   [SerializeField] private HouseBoss _houseboss;

   private int _woodamount = 0;

   private int _stoneamount =0;

   private int _foodamount = 0;

   public int _npcamount = 0;

   public int _maxnpcamount = 0;


   void Start()
    {
        UpdateUI();

    }

   public void _addwood(int amount)
    {
        _woodamount += amount;
        UpdateUI();

    }

    public void _addstone(int amount)
    {
        _stoneamount += amount;
        UpdateUI();
    }

    public void _addfood(int amount)
    {
        _foodamount += amount;
        UpdateUI();
    }

    public bool _usewood(int cost)
    {
        if(_woodamount >= cost)
        {
            _woodamount -= cost;
            UpdateUI();
            return true;
        }
        Debug.Log("you to pool");
        return false;
    }


        public bool _usestone(int cost)
    {
        if(_stoneamount >= cost)
        {
            _stoneamount -= cost;
            UpdateUI();
            return true;
        }
        Debug.Log("you to pool");
        return false;


        
    }

    public bool _usefood(int cost)
    {
        if(_foodamount >= cost)
        {
            _foodamount -= cost;
            UpdateUI();
            return true;
        }
        Debug.Log("you to pool");
        return false;
    }

    public void _npc()
    {
        StartCoroutine(_Delaynpc());
    }

    private IEnumerator _Delaynpc()
    {
        yield return null;
        int npccount = GameObject.FindGameObjectsWithTag("npc").Length;
       _npcamount = npccount;
       UpdateUI();

    }

    public void _addmaxnpc(int amount)
    {
        _maxnpcamount += amount;
        UpdateUI();
    }
   public void UpdateUI()
    {
        _woodUI.text= "wood:" + _woodamount;
        _stoneUI.text = "stone:" + _stoneamount;
        _foodUI.text = "food:" + _foodamount;
        _NPCUI.text = "NPC:" + _npcamount + "/" + _maxnpcamount;
    }

}
