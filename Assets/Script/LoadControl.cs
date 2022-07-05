using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{
    private List<PlayerItem> playerInventory;
    
    private GameObject clickedButton;
    private Text objekTextButton;
    public static string namaButton;
    public static bool isLoad;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    [SerializeField]
    private Sprite[] iconSprites;

    public int fileNum;
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo d =  new DirectoryInfo(Application.dataPath + "/save/");
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("roomdesign"))
                fileNum++;
        }
        GenInventory();
    }

    void GenInventory()
    {
            string namaFile = "";
            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/save/");
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                if (Path.GetExtension(fi.ToString())==".roomdesign")
                {
                    namaFile = Path.GetFileNameWithoutExtension(fi.ToString());
                    GameObject newButton = Instantiate(buttonTemplate) as GameObject;
                    newButton.SetActive(true);
                    Text ButtonText = newButton.GetComponentInChildren<Text>();
                    ButtonText.text = namaFile;
                    //newButton.GetComponent<LoadButton>().SetIcon(newItem.iconSprite);
                    newButton.transform.SetParent(buttonTemplate.transform.parent, false);
                }
            }
    }

    public void clickedLoad()
    {
        clickedButton = EventSystem.current.currentSelectedGameObject;
        objekTextButton = clickedButton.GetComponentInChildren<Text>();
        namaButton = objekTextButton.text;
        isLoad = true;
        SceneManager.LoadScene("buildScene");
    }

    public void clickedView()
    {
        clickedButton = EventSystem.current.currentSelectedGameObject;
        objekTextButton = clickedButton.GetComponentInChildren<Text>();
        namaButton = objekTextButton.text;
        isLoad = true;
        SceneManager.LoadScene("viewScene");
    }

    public class PlayerItem
    {
        public Sprite iconSprite;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
