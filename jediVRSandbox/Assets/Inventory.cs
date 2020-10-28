using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Script based on various tutorials and code 
// from Unity Documentation and Unity Forums
public class Inventory : MonoBehaviour
{
	public GameObject inventoryUI;
	private List<GameObject> inventoryList;
	private bool showUI = false;
	private GameObject inventoryPanel;
	
    void Start()
    {
        if(inventoryList == null){
			inventoryList = new List<GameObject>();
		}
		
		if(inventoryUI){
			showUI = true;
			foreach(Transform child in transform){
				if(child.name == "InventoryUI")
					inventoryPanel = child.transform.GetChild(0).gameObject;
			}
		}
    }
	
    void Update()
    {
		if(Input.GetKeyDown("i") || (OVRInput.Get(OVRInput.RawButton.X))){
			//show inventory
			showUI = !showUI;
		}
		showInventoryUI();
    }
	
	void showInventoryUI(){
		if(inventoryUI){
			inventoryUI.SetActive(showUI);
			
			if(showUI){
				//show objects in inventory
				int indx = 0;
				Debug.Log(inventoryPanel);
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
}
