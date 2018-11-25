using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        Cursor.visible = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 pos = transform.position;

          transform.position = Vector2.Lerp(pos, mousePos, Time.deltaTime*3);

       
       
	}
}
