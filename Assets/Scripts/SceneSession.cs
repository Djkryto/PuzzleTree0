using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSession : MonoBehaviour
{
    public static SceneSession Instance;

    private List<ItemTransformData> _interactiveItems;
    private Player _player;

    public List<ItemTransformData> InteractiveItems => _interactiveItems;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        ColletAllItemsOnScene();
        _player = FindObjectOfType<Player>();
        _player.TakedItem += OnTakeItem;
        _player.DropedItem += OnDropItem;
        SceneSaver sceneSaver = new SceneSaver(SceneManager.GetActiveScene().name, this);
        sceneSaver.Load(ref _interactiveItems);
    }

    private void OnTakeItem(InteractiveItem item)
    {
        var currentItem = _interactiveItems.FirstOrDefault(itemInCollection => itemInCollection.Item == item);
        _interactiveItems?.Remove(currentItem);
    }

    private void OnDropItem(InteractiveItem item)
    {
        int itemIndex = Contains(item);
        if(itemIndex == -1)
        {
            var itemData = new ItemTransformData(item, item.transform.position);
            _interactiveItems.Add(itemData);
        }
        else
        {
            var itemData = _interactiveItems[itemIndex];

            itemData.ItemPosition = new Position(item.transform.position);
            _interactiveItems[itemIndex] = itemData;
        }
    }

    private int Contains(InteractiveItem item)
    {
        for(int i = 0; i < _interactiveItems.Count; i++)
        {
            if (_interactiveItems[i].Item == item)
            {
                return i;
            }
        }
        return -1;
    }

    private void ColletAllItemsOnScene()
    {
        var interactiveItemsArray = FindObjectsOfType<InteractiveItem>();
        _interactiveItems = new List<ItemTransformData>();

        foreach(var item in interactiveItemsArray)
        {
            var itemData = new ItemTransformData(item, item.transform.position);
            _interactiveItems.Add(itemData);
        }
    }

    private void OnApplicationQuit()
    {
        SceneSaver sceneSaver = new SceneSaver(SceneManager.GetActiveScene().name, this);
        sceneSaver.Save();
    }
}


