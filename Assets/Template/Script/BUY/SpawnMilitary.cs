using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMilitary : MonoBehaviour
{
    [SerializeField] private GameObject _soldier;
    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private GameObject _MilitaryBUYUI;
    [SerializeField] private GameObject _buyUI;

    // ทำตัวแปร Static ไว้เก็บว่า "ตอนนี้ตึกหลังไหนกำลังถูกเลือกอยู่"
    public static SpawnMilitary CurrentSelectedBase;

    void Start()
    {
        if(_itemUI == null)
        {
           _itemUI = FindObjectOfType<ItemUI>();
        }

        if(_MilitaryBUYUI == null)
        {
           _MilitaryBUYUI = MilitaryUIManager.MilitaryUI;
        }
        if(_buyUI == null)
        {
           _buyUI = BUYUIManager.BUYUI;
        }
    }

    private void OnMouseDown()
    {
        // พอกลุ๊กคลิกที่ตึกนี้ ให้ตั้งค่าให้ตึกนี้เป็น "ตึกปัจจุบันที่ถูกเลือก"
        CurrentSelectedBase = this;
        openBUY();
    }

    public void openBUY()
    {
        CurrentSelectedBase = this; // ตั้งค่ากันเหนียวตอนเปิด UI

        if (_MilitaryBUYUI != null)
        {
            _MilitaryBUYUI.SetActive(true);
            if (_buyUI != null)
            {
                _buyUI.SetActive(false);
            }
        }
        else
        {
            Debug.Log("i dont see");
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกจากปุ่ม UI (สามารถใช้ปุ่มเดิมได้เลย)
    public void BUYsoldier()
    {
        // เช็คว่ามีตึกที่ถูกเลือกอยู่ไหม ถ้ามี ให้ใช้ตำแหน่งของตึกนั้นแทนตัว (0,0,0)
        SpawnMilitary targetBase = CurrentSelectedBase != null ? CurrentSelectedBase : this;

        Vector3 _spawnpos = targetBase.transform.position;
        Debug.Log("SpawnMilitary อยู่ที่: " + _spawnpos);

        if(_itemUI == null)
        {
            _itemUI = FindObjectOfType<ItemUI>();
        }

        if(_itemUI != null)
        {
            int cost = 1;

            if(_itemUI._npcamount >= _itemUI._maxnpcamount)
            {
                Debug.Log("full!");
                return;
            }

            if(!_itemUI._usefood(cost))
            {
                Debug.Log("cant buy");  
                return;
            }

            Vector3 spawnPos = _spawnpos + new Vector3(0, 2, 0);
            GameObject newsoldier = Instantiate(_soldier, spawnPos, quaternion.identity);
            Debug.Log("soldier spawned at: " + spawnPos);
            _itemUI._npc();
        }
    }
}