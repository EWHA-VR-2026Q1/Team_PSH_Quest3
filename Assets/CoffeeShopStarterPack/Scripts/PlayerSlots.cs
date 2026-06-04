using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace PW
{
    public class PlayerSlots : MonoBehaviour
    {
        public int slotCount;
        int[] slotItems;
        public Image[] slotUIObjects;

        private void OnEnable()
        {
            if (slotItems == null)
                slotItems = new int[3] { -1, -1, -1 };

            BasicGameEvents.onProductAddedToSlot += BasicGameEvents_onProductAddedToSlot;
            BasicGameEvents.onProductDeletedFromSlot += BasicGameEvents_onProductDeletedFromSlot;
        }

        private void BasicGameEvents_onProductDeletedFromSlot(int ID)
        {
            if (slotItems != null && slotItems.Length > 0)
                slotItems[ID] = -1;
        }

        private void BasicGameEvents_onProductAddedToSlot(int orderID)
        {
            var orderGenerator = FindObjectOfType<OrderGenerator>();
            var emptyIndex = Array.IndexOf(slotItems, -1);
            slotItems[emptyIndex] = orderID;
            slotUIObjects[emptyIndex].sprite = orderGenerator.GetSpriteForOrder(orderID);
            StartCoroutine(DoEmphasize(emptyIndex));
        }

        public IEnumerator DoEmphasize(int index)
        {
            var uiImage = slotUIObjects[index];
            float totalTime = .6f;
            float curTime = totalTime;
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;
                uiImage.transform.localScale += Vector3.one * 0.1f * -1f * Mathf.Sin(totalTime - 2 * curTime);
                yield return null;
            }
            uiImage.transform.localScale = Vector3.one;
        }

        private void OnDisable()
        {
            BasicGameEvents.onProductAddedToSlot -= BasicGameEvents_onProductAddedToSlot;
            BasicGameEvents.onProductDeletedFromSlot -= BasicGameEvents_onProductDeletedFromSlot;
        }

        public bool CanHoldItem(int orderID)
        {
            var emptyIndex = Array.IndexOf(slotItems, -1);
            return emptyIndex >= 0;
        }

        public bool HoldsItem(int orderID)
        {
            int indexofOrder = Array.IndexOf(slotItems, orderID);
            if (indexofOrder == -1) return false;

            slotUIObjects[indexofOrder].sprite = null;
            slotItems[indexofOrder] = -1;
            return true;
        }
    }
}