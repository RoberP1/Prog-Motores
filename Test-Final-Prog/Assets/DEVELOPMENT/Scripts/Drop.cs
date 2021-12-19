using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    private IInventory inventory;
    private void Start() => inventory = FindObjectOfType<IInventory>();
   
    public void OnDrop(PointerEventData eventData) => inventory.DropSelected();

}
