using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] public GameObject st1;
    [SerializeField] private GameObject st2;
    [SerializeField] private GameObject st3;
    [SerializeField] public GameObject st4;

    
    public bool _havestable = false;



    void Start()
    {
        if(st1.activeSelf == true)
        {
            StartCoroutine(_growing());
            _havestable = false;
        }
        
    }


    private IEnumerator _growing()
    {
        
        yield return new WaitForSeconds(Random.Range(1f,2f));
        st1.SetActive(false);
        st2.SetActive(true);
        yield return new WaitForSeconds(Random.Range(1f,2f));
        st2.SetActive(false);
        st3.SetActive(true);
        yield return new WaitForSeconds(Random.Range(2f,3f));
        st3.SetActive(false);
        st4.SetActive(true);
        _havestable = true;
    }

    public void _regrow()
    {
        st4.SetActive(false);
        st1.SetActive(true);
        _havestable = false;
        StartCoroutine(_growing());
    }



}
