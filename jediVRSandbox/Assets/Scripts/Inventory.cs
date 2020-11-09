using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
	DESCRIPTION: This Script is the inventory system for collecting items. 
	COMPATIBLE SCRIPTS: FindItem.cs, UnlockItem.cs
	DIRECTIONS:	In order to show the UI, you have to connect a UI object to the 
		script. Ensure that this UI object contains a child object called 
		"InventoryPanel". If there is a UI object, the player can toggle the 
		UI object OFF and ON by pressing either the "i" button on a keyboard 
		or the "Y" button on an Oculus.
	PROJECTED CHANGES: Currently the inventory is displayed on a wall. However,
		the ideal method is for the inventory to display on the player's camera
		when they wish to see it.
*/

//Script based on various tutorials and code 
// from Unity Documentation and Unity Forums
public class Inventory : MonoBehaviour
{
	public GameObject inventoryUI;
	//public Camera cam;
	private List<GameObject> inventoryList;
	private bool showUI = false;
	private GameObject inventoryPanel;
	private Vector3 viewPos;
	
    void Start()
    {
        if(inventoryList == null){
			inventoryList = new List<GameObject>();
		}
		
		if(inventoryUI){
			showUI = true;
			foreach(Transform child in inventoryUI.transform){
				if(child.name == "InventoryPanel")
					inventoryPanel = child.gameObject;
			}
		}
    }
	
    void Update()
    {
		ToggleUI();			
		showInventoryUI();
    }
	
	void showInventoryUI(){
		if(inventoryUI){
			inventoryPanel.SetActive(showUI);
			// https://stackoverflow.com/questions/38695900/keeping-a-objectworld-space-canvas-always-in-the-cameras-viewport
			//Vector3 target = inventoryUI.transform.GetWorldCorners();
			//Vector3 uiPos = inventoryUI.transform.position;
			//viewPos = cam.WorldToViewportPoint(uiPos);
			//uiPos = new Vector3(uiPos.x - viewPos.x/2, uiPos.y - viewPos.y/2, uiPos.z);
			// inventoryUI.transform.position = uiPos;
			
			if(showUI){
				//show objects in inventory
				int indx = 0;
				//foreach(Transform slot in inventoryPanel.transform){
				for(int i = 0; i<inventoryPanel.transform.childCount; i++){
					GameObject itemImage = inventoryPanel.transform.GetChild(i).GetChild(0).gameObject;
					GameObject text = inventoryPanel.transform.GetChild(i).GetChild(1).gameObject;
										
					if(indx < inventoryList.Count){
						GameObject slotItem = inventoryList[i].gameObject;
						//Set image and name for item in inventory
						itemImage.GetComponent<Image>().sprite = slotItem.GetComponent<FindItem>().itemIcon;
						itemImage.GetComponent<Image>().color = new Color(0,0,0,1);

						text.GetComponent<Text>().text = slotItem.name;
					}
					else{
						//make them blank
						itemImage.GetComponent<Image>().sprite = null;
						itemImage.GetComponent<Image>().color = new Color(0,0,0,0);
						text.GetComponent<Text>().text = "";
					}
				}
			}
		}
	}
	
	public void AddItem(GameObject obj){
		inventoryList.Add(obj);
		obj.SetActive(false);
	}
	
	public bool HasItem(GameObject obj, bool removeUsed){
		if(inventoryList.Contains(obj)){
			if(removeUsed){
				UseItem(obj);
			}
			return true;
		}
		else
			return false;
	}
	
	private void UseItem(GameObject obj){
		inventoryList.Remove(obj);
	}
	
	private void ToggleUI(){
		if((Input.GetKeyDown("i") || (OVRInput.Get(OVRInput.RawButton.Y)))){
			//show inventory
			if(showUI == true)
			{
				showUI = false;
			}
			else
			{
				showUI = true; 
			}        
		}
	}
}
