using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	DESCRIPTION: This script is for "unlocking" items such doors or boxes.
	COMPATIBLE SCRIPTS: Inventory.cs, FindItem.cs
	DIRECTIONS:	Place this script on any object that needs to be unlocked.
		Ensure that the object has a at least 2 children (a lid/door and 
		a hinge) so that the lid/door can rotate around a hinge. Also 
		input the key (an object that will unlock the box/door) and ensure
		the key object has the FindItem.cs script attached to it.
	PROJECTED CHANGES: This script currently only works for boxes. It will
		eventually be changed to work with doors.
*/

//Script based on various tutorials and code 
// from Unity Documentation and Unity Forums
public class UnlockItem : MonoBehaviour
{
	public GameObject player; //Input the player object
	public GameObject key; //Input the key object
	public int DistanceFromObjectRadius = 5;
	private GameObject lid;
	private GameObject hinge;
	private bool open = false;
	private Vector3 hitRadius;
	private Inventory inventory;
	public Animator animator;

	void Start()
    {
		if(name == "box"){
			//https://answers.unity.com/questions/183649/how-to-find-a-child-gameobject-by-name.html
			foreach(Transform child in transform){
				if(child.name == "lid")
					lid = child.gameObject;
				if(child.name == "hinge"){
					hinge = child.gameObject;
				}
			}
		}
		
		hitRadius = new Vector3(DistanceFromObjectRadius, DistanceFromObjectRadius,DistanceFromObjectRadius);
		
		inventory = player.GetComponent<Inventory>();
    }
	
	void Update(){
		if(OVRInput.Get(OVRInput.RawButton.X)){
			OnMouseDown();
		}
	}
	
	void OnMouseDown(){
		if(inventory.HasItem(key, true)){		
			//https://answers.unity.com/questions/1699266/how-do-you-check-if-a-game-object-is-in-the-radius.html
			if(Vector3.Distance(transform.position, player.transform.position) < 5 && !open){
				open = true;

				//rotate lid
				animator.SetBool("isOpen", true);
				lid.transform.Translate(hinge.transform.localPosition);
			   lid.transform.Rotate(90, 0, 0);
			}
		}
	}
}
