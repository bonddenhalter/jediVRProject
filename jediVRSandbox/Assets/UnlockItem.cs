using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if(OVRInput.Get(OVRInput.RawButton.B)){
			OnMouseDown();
		}
	}
	
	void OnMouseDown(){
		if(inventory.HasItem(key, true)){		
			//https://answers.unity.com/questions/1699266/how-do-you-check-if-a-game-object-is-in-the-radius.html
			if(Vector3.Distance(transform.position, player.transform.position) < 5 && !open){
				open = true;
				
			   //rotate lid
			   lid.transform.Translate(hinge.transform.localPosition);
			   lid.transform.Rotate(90, 0, 0);
			}
		}
	}
}
