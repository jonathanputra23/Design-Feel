using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    private List<PlayerItem> playerInventory;
    public Dropdown dropdownMenu;
    public string currDD;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    [SerializeField]
    private Sprite[] basicSprites;

    [SerializeField]
    private string[] basicPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        currDD = dropdownMenu.GetComponent<Dropdown>().captionText.text;
        playerInventory = new List<PlayerItem>();

        for(int i = 0; i< basicSprites.Length; i++)
        {
            PlayerItem newItem = new PlayerItem();
            newItem.iconSprite = basicSprites[i];
            newItem.itemName = basicPrefabs[i];
            playerInventory.Add(newItem);
        }
        GenInventory();
    }
    
    void GenInventory()
    {
        if (playerInventory.Count < 6)
        {
            gridGroup.constraintCount = playerInventory.Count;
        }
        else
        {
            gridGroup.constraintCount = 5;
        }
        foreach(PlayerItem newItem in playerInventory)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);

            newButton.GetComponent<InventoryButton>().SetIcon(newItem.iconSprite);
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public class PlayerItem
    {
        public Sprite iconSprite;
        public string itemName;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
