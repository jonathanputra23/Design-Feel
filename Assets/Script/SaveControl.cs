using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveControl : MonoBehaviour
{
    public bool loadFile = false;
    public bool newFile = false;
    public int width;
    public int length;
    public string loadName;
    public Text saveName;
    public GameObject groundObject;
    public GameObject prefabs;
    public ObjectData data = new ObjectData();
    public void Start()
    {
        loadFile = LoadControl.isLoad;
        newFile = LoadSceneOnClick.isNew;
        checkIfLoad();
        checkIfNew();
        if (!Directory.Exists(Application.dataPath + "/save/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/save/");
        }
    }
    public void Save()
    {
        GameObject[] furnitureGO = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject furniture in furnitureGO)
        {
            String objectMesh = furniture.transform.name;
            Transform currFurniture = furniture.transform;
            Vector3 pos = currFurniture.position;
            GameObjectSaveData GOSD = new GameObjectSaveData(objectMesh, pos.x, pos.y, pos.z, currFurniture.rotation.eulerAngles.x, currFurniture.rotation.eulerAngles.y, currFurniture.rotation.eulerAngles.z);
            data.objects.Add(GOSD);
        }
        data.lengthGround = length;
        data.widthGround = width;
        BinaryFormatter bf = new BinaryFormatter();
        string saveFileDest = saveName.text.ToString();
        FileStream file = File.Create(Application.dataPath + "/save/"+ saveFileDest + ".roomdesign");
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.dataPath + "/save/" + loadName +".roomdesign"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/save/" + loadName + ".roomdesign", FileMode.Open);
            ObjectData loadData = (ObjectData)bf.Deserialize(file);
            file.Close();
            GameObject[] furnitureGO = GameObject.FindGameObjectsWithTag("Furniture");
            foreach (GameObject furniture in furnitureGO)
            {
                Destroy(furniture);
            }
            for (int i = 0; i<loadData.objects.Count; i++)
            {
                prefabs = (GameObject)Resources.Load("Prefabs/BuildSystemPrefabs/" + loadData.objects[i].prefab);
                Debug.Log(loadData.objects[i].prefab);
                Vector3 t = new Vector3(loadData.objects[i].posX, loadData.objects[i].posY, loadData.objects[i].posZ);
                Vector3 r = new Vector3(loadData.objects[i].rotX, loadData.objects[i].rotY, loadData.objects[i].rotZ);
                GameObject go = Instantiate(prefabs, t, Quaternion.Euler(r));
                go.name = prefabs.name;
            }
            width = loadData.widthGround;
            length = loadData.lengthGround;
            groundObject.transform.localScale = new Vector3(width, 1, length);
            loadFile = false;
        }
    }

    public void checkIfLoad()
    {
        if (loadFile)
        {
            loadName = LoadControl.namaButton;
            Load();
        }
    }

    public void checkIfNew()
    {
        if (newFile)
        {
            width = LoadSceneOnClick.width;
            length = LoadSceneOnClick.length;
            groundObject.transform.localScale = new Vector3(width, 1, length);
            newFile = false;
        }
    }
}

[System.Serializable]
public class GameObjectSaveData
{
    public float posX;
    public float posY;
    public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;
    public string prefab;
    public GameObjectSaveData(string prefabname, float px, float py, float pz, float rx, float ry, float rz)
    {
        this.prefab = prefabname;
        this.posX = px;
        this.posY = py;
        this.posZ = pz;
        this.rotX = rx;
        this.rotY = ry;
        this.rotZ = rz;
    }
}

[System.Serializable]
public class ObjectData
{
    public List<GameObjectSaveData> objects;
    public int widthGround;
    public int lengthGround;
    public ObjectData()
    {
        this.objects = new List<GameObjectSaveData>();
    }
}

