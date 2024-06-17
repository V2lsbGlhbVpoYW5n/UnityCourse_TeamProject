using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour {

	public bool selected = false;   //对象是不是已经被选中，
	private bool isSelecting = false;

	public bool Selected(){ return selected; }

	public void SetSelected()
	{
		selected = !selected;
	}
	
	public void Select()
	{
		isSelecting = true;
		foreach (var selection in GetComponents<InteractionLogic>())
		{
			if (selection)
			{
				selection.Select();
			}
		}
	}

	public void Deselect()
	{
		foreach (var selection in GetComponents<InteractionLogic>())
		{
			if (selection)
			{
				selection.Deselect();
			}
		}
		isSelecting = false;
	}
	
	// Use this for initialization
	void Start () {
	
	}

	public bool GetSelected()
	{
		return selected;
	}
	
	// Update is called once per frame
	void Update () {
		if (selected)
		{
			if (!isSelecting)
			{
				Select();
			}
		}
		else
		{
			if (isSelecting)
			{
				Deselect();
			}
		}
	}
}
