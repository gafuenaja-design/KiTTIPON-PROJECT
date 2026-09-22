using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsRTS : MonoBehaviour
{
    private GameObject _PickedGameObject;
    private IMovePosition _movePosition;


    private void Awake()
    {
        _PickedGameObject = transform.Find("Picked").gameObject;
        _movePosition = GetComponent<IMovePosition>();
        setpickedtrue(false);
    }
    public void setpickedtrue(bool visible)
    {
        if(_PickedGameObject != null)
        {
            _PickedGameObject.SetActive(visible);
        }
    }
   
   public void MoveTo(Vector3 targetPosition)
    {
        _movePosition.setmoveposition(targetPosition);
    }
  
}
