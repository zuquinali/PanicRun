using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour {

    public GameObject _character;
    public int _displacement = 1;
    public Camera _camera;

    private int _now;
    private int _count;

    // Use this for initialization
    void Start () {
        this._now = System.DateTime.Now.Millisecond;
        this._count = 100;
    }
	
	// Update is called once per frame
	void Update () {
        //TODO: Refazer essa conta
        int milliseconds = System.DateTime.Now.Millisecond;

        if ( (milliseconds - this._now) >= this._count )
        {
            moveCharacter();

            this._now = milliseconds;
        }
    }

    void moveCharacter()
    {
        _character.transform.position = new Vector3(_character.transform.position.x + _displacement, _character.transform.position.y, _character.transform.position.z);
        _camera.transform.position = new Vector3(_camera.transform.position.x + _displacement, _camera.transform.position.y, _camera.transform.position.z);
    }
}
