using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{

    public GameObject prefab;//the prefab that represents this preview ie, a foundation, wall....

    private MeshRenderer myRend;
    public Material goodMat;//green material
    public Material badMat;//red material

    private BuildSystem buildSystem;

    private bool isSnapped = false;//only this script should change this value

    public bool isFoundation = false;//this is a special rule for foundations. 
                                       //the first foundation that you build in your game 
                                       //will not have anything to snap too, so you wont be able to build it
                                       //with this bool check you are saying ALL foundations dont need to be 
                                       //snapped to anything inorder to be built

    public List<string> tagsISnapTo = new List<string>();//list of all of the SnapPoint tags this particular preview can snap too
                                                         //this allows this previewObject to be able to snap to multiple snap points



    private void Start()
    {
        buildSystem = GameObject.FindObjectOfType<BuildSystem>();
        myRend = GetComponent<MeshRenderer>();
        ChangeColor();
    }

    public void Place()
    {
        GameObject go = Instantiate(prefab, transform.position, transform.rotation);
        go.name = prefab.name;
        Destroy(gameObject);
    }

    private void ChangeColor()//changes between red and greed depending if this preview is/is not snapped to anything
    {
        if (isSnapped)
        {
            myRend.material = goodMat;//just changing the MeshRenderers material to the goodMat(green)
        }
        else
        {
            myRend.material = badMat;//just changing the MeshRenderers material to the badMat(red)
        }

        if (isFoundation)//this is the foundation rule that was added earlier
        {
            myRend.material = goodMat;
            isSnapped = true;//we have to force this bool here, because the BuildSystem.cs requires this to be true before you can build
        }


    }

    private void OnTriggerEnter(Collider other)//this is what dertermins if you are snapped to a snap point
    {
        for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all the tags this preview can snap too
        {
            string currentTag = tagsISnapTo[i];//setting the current tag were looking at to a string...its easier to write currentTag then tagsISnapTo[i]

            if(other.tag == currentTag)
            {
                buildSystem.PauseBuild(true);//this, and the line below are how you snap
                                             //since we are using a raycast to position the preview
                                             //when you snap to something we need to "pause" the raycast
                                             //otherwise the position will get overridden next frame,
                                             //and it will look like the preview never snapped to anything
                                             //pay attention to the stickTolerance and pauseBuilding in the 
                                             //build system to see how to unpause the build system raycast

                transform.position = other.transform.position;//set position of preview so that it "snaps" into position
                isSnapped = true;//change the bool so we know what color this preview needs to be
                ChangeColor();
            }

        }
    }

    private void OnTriggerExit(Collider other)//this is what determins if you are no longer snapped to a snap point
    {
        for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all tags
        {
            string currentTag = tagsISnapTo[i];

            if(other.tag == currentTag)//if we OnTriggerExit something that we can snap too
            {
                isSnapped = false;//were no longer snapped
                ChangeColor();//change color
            }
        }
    }


    public bool GetSnapped()//accessor for the isSnapped bool. 
    {
        return isSnapped;
    }




}
