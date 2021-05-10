using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;  //�Ů�ID ���� ���~ID
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemSlot;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if(item == null)
        {
            itemSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}
