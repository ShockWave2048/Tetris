using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellViewController : MonoBehaviour {

    public GameObject model;
    public List<Material> Materials = new List<Material>(7);
    public int _currentMatID = -1;
    public Material currentMaterial = null;

    public int CurrentMatID
    {
        get
        {
            return _currentMatID;
        }

        set
        {
            _currentMatID = value;
            if (_currentMatID > -1)
                model.GetComponent<Renderer>().sharedMaterial = Materials[_currentMatID];
        }
    }

    void Start () {
        if ( _currentMatID > -1)
            model.GetComponent<Renderer>().sharedMaterial = Materials[_currentMatID];
    }
	
	void Update () {
		
	}
}
