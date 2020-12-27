using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public TextAsset dialogJson;
    public CharacterDialogs characterDialogs;
    private Camera dialogCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        //locate camera and disable
        dialogCamera = GameObject.Find("Dialog Camera").GetComponent<Camera>();
        dialogCamera.gameObject.SetActive(false);

        //read json file and store in class
        characterDialogs = JsonUtility.FromJson<CharacterDialogs>(dialogJson.text);
    }
    // Update is called once per frame
    //void Update() {}
}

//classes for the json file
[System.Serializable]
public class CharacterDialog
{
    //inner class, holds the variables for each 'dialog' object
    public string character;
    public string sentence;
    public string trigger;
}
[System.Serializable]
public class CharacterDialogs
{
    //outer class, holds a list that contains all the dialog objects
    public CharacterDialog[] dialog;
}



