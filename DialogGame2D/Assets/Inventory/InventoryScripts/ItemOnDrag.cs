using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;  //當前物品的ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false; //射線阻擋關閉
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);     //輸出鼠標當前位置下到第一個碰到物體名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")     //判斷下面物體名字是:Item Image 那麼互換位置
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itemList的物品存儲位置改變
                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;


                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;  //設限阻擋開啟，不然無法再次選中移動的物品
                return;
            }
            if(eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                //否則直接掛在檢測到Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itemList的物品存儲位置改變
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];

                if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                    myBag.itemList[currentItemID] = null;


                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        //其他任何位置都歸位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
