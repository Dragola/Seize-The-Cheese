using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public TextAsset dialogJson;
    public CharacterDialogs characterDialogs;
    public Text dialogText = null;
    public string firstSentence = "";
    public int stringIndex = 0;
    public float timer = 0f;
    public bool writeText = false;
    public float textPrintRate = 0.25f;
    private PlayerController player = null;

    // Start is called before the first frame update
    void Start()
    {
        //reference and set text to blank
        dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
        dialogText.text = "";

        //only get json file if variable isn't null
        if (dialogJson != null)
        {
            //read json file and store in class
            characterDialogs = JsonUtility.FromJson<CharacterDialogs>(dialogJson.text);
            
            //get first sentence (Sir Bitsy) for testing
            firstSentence = characterDialogs.dialog[0].sentence;
        }
    }
    void Update()
    {
        //write text
        if (writeText)
        {
            //timer for printing text
            timer += Time.deltaTime;
            
            //print the next letter once timer hits print rate
            if(timer >= textPrintRate && stringIndex < firstSentence.Length + 1)
            {
                //print next letter of sentence
                dialogText.text = firstSentence.Substring(0, stringIndex);
                
                //increment index and reset timer
                stringIndex += 1;
                timer = 0f;
            }
            //stop printing text
            else if (stringIndex >= firstSentence.Length + 1)
            {
                //reset variables and set text to blank
                writeText = false;
                stringIndex = 0;
                timer = 0f;
                dialogText.text = "";

                //re-enabled PlayerController and disable dialog gameobject
                player.enabled = true;
                this.gameObject.SetActive(false);
            }
        }
    }
    public void WriteText(GameObject player)
    {
        //only enable writing text if not already enabled
        if (writeText == false)
        {
            writeText = true;
            //reference PlayerController script and disable to prevent player from moving (WARNING- player stay in air if activated [aka gravity is disabled])
            this.player = player.GetComponent<PlayerController>();
            this.player.enabled = false;
        }
    }
}
//classes for the json file
[System.Serializable]
public class CharacterDialog
{
    //inner class, holds the variables for each 'dialog' object
    public string character;    //the character who is speaking
    public string sentence;     //the sentence the character says
    public string trigger;      //the tigger for the dialog (prevent all dialog from printing)
}
[System.Serializable]
public class CharacterDialogs
{
    //outer class, holds the list that contains all the dialog objects
    public CharacterDialog[] dialog;
}



