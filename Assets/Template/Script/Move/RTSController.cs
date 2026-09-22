using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class RTSController : MonoBehaviour
{

    [SerializeField] private Transform _selectionAreaTransform;
    private Vector3 _startPosition;
    private List<UnitsRTS> _selectedUnitsList;

    private void Awake()
    {
        _selectedUnitsList = new List<UnitsRTS>();
        _selectionAreaTransform.gameObject.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectionAreaTransform.gameObject.SetActive(true);
            _startPosition = UtilsClass.GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 _currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 _lowerLeft = new Vector3(Mathf.Min(_startPosition.x, _currentMousePosition.x), Mathf.Min(_startPosition.y, _currentMousePosition.y));
            Vector3 _upperRight = new Vector3(Mathf.Max(_startPosition.x, _currentMousePosition.x), Mathf.Max(_startPosition.y, _currentMousePosition.y));
            _selectionAreaTransform.position = _lowerLeft;
            _selectionAreaTransform.localScale = _upperRight - _lowerLeft;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _selectionAreaTransform.gameObject.SetActive(false);
            Collider2D[] _colliders2DArray = Physics2D.OverlapAreaAll(_startPosition, UtilsClass.GetMouseWorldPosition());
            foreach (UnitsRTS unitRTS in _selectedUnitsList)
            {
                unitRTS.setpickedtrue(false);
            }
            _selectedUnitsList.Clear();

            foreach (Collider2D collider2D in _colliders2DArray)
            {
                UnitsRTS unitsRTS = collider2D.GetComponent<UnitsRTS>();
                if (unitsRTS != null)
                {
                    unitsRTS.setpickedtrue(true);
                    _selectedUnitsList.Add(unitsRTS);
                }
            }
//            Debug.Log(_selectedUnitsList.Count);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
            List<Vector3>targetpositionlist = GetPositionListAround(targetPosition,new float[] {1.5f,3f,4.5f} , new int[] {5,10,20});    
            int targetpositionlistindex = 0;


            foreach (UnitsRTS unitsRTS in _selectedUnitsList)
           {
          if (unitsRTS == null)
          continue;

          unitsRTS.MoveTo(
             targetpositionlist[targetpositionlistindex]
            );

           targetpositionlistindex =
           (targetpositionlistindex + 1)
           % targetpositionlist.Count;
         }
        }
    }
    private List<Vector3> GetPositionListAround(Vector3 startposition, float[] ringdistancearray, int[] ringpositioncountarry)
    {
        List<Vector3> positionlist =new List<Vector3>();
        positionlist.Add(startposition);
        for(int i = 0 ; i < ringdistancearray.Length; i++)
        {
            positionlist.AddRange(GetPositionListAround(startposition, ringdistancearray[i] , ringpositioncountarry[i]));

        }
        return positionlist;
    }

    private List<Vector3>GetPositionListAround(Vector3 startposition, float distance , int positioncount)
    {
        List<Vector3>positionlist = new List<Vector3>();

        for(int i =0 ; i < positioncount; i++)
        {
            float angle = i*(360f/positioncount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1,0) , angle);
            Vector3 position = startposition + dir * distance;
            positionlist.Add(position);
        }
        return positionlist;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec,float angle )
    {
        return Quaternion.Euler(0,0 ,angle) * vec;
    }
}
