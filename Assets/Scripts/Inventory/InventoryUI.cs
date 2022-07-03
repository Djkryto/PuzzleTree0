using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private HotBar _hotbar;

    private void Start()
    {
        _inventory.gameObject.SetActive(false);
        _hotbar.gameObject.SetActive(false);
    }
}
