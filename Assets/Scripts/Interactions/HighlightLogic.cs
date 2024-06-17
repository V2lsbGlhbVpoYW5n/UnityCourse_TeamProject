using System;
using UnityEngine;
using System.Collections;

public class HighlightLogic : InteractionLogic {

	public GameObject displayItem;
	
	public override void Deselect ()
	{
		displayItem.SetActive (false);
	}

	public override void Select ()
	{
		displayItem.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		displayItem.SetActive (false);
	}
	

	private void LateUpdate()
	{
		displayItem.transform.eulerAngles = new Vector3(0f, 0f, 0f);
	}
}
