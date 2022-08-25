using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSession : MonoBehaviour
{
    public static SceneSession Instance;
    public Action<SceneData> SceneClosed;

    public string _sceneId;
    private List<ItemTransformData> _interactiveItems;
    private Player _player;

    public string SceneId => _sceneId;
    public Player Player => _player;
    public List<ItemTransformData> InteractiveItems => _interactiveItems;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _sceneId = SceneManager.GetActiveScene().name;
        ColletAllItemsOnScene();
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
        _player = FindObjectOfType<Player>();
        _player.TakedItem += OnTakeItem;
        _player.DropedItem += OnDropItem;
    }

    private void OnTakeItem(InteractiveItem item)
    {
        var currentItem = _interactiveItems.FirstOrDefault(itemInCollection => itemInCollection.Name == item.name);
        _interactiveItems?.Remove(currentItem);
    }

    private void OnDropItem(InteractiveItem item)
    {
        int itemIndex = Contains(item);
        if (itemIndex == -1)
        {
            var itemData = new ItemTransformData(item, item.transform.position);
            _interactiveItems.Add(itemData);
        }
        else
        {
            var itemData = _interactiveItems[itemIndex];

            itemData.Position = new Position(item.transform.position);
            _interactiveItems[itemIndex] = itemData;
        }
    }

    private int Contains(InteractiveItem item)
    {
        for (int i = 0; i < _interactiveItems.Count; i++)
        {
            if (_interactiveItems[i].Name == item.name)
            {
                return i;
            }
        }
        return -1;
    }

    private void OnDestroy()
    {
        ColectSceneData();
    }

    public SceneData ColectSceneData()
    {
        SceneData sceneData = new SceneData();
        foreach (var item in _interactiveItems)
        {
            sceneData.Items.Add(item);
        }
        SceneClosed?.Invoke(sceneData);
        return sceneData;
    }
}


