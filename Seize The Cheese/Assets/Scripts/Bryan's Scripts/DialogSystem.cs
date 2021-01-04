using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public TextAsset dialogJson;
    public CharacterDialogs characterDialogs;
    public Text dialogText = null;
    public int stringIndex = 0;
    public float timer = 0f;
    public bool writeText = false;
    public bool inDialogMode = false;
    public float textPrintRate = 0.25f;
    private PlayerController player = null;
    public List<CharacterDialog> dialogsToPrint = new List<CharacterDialog>();
    public CharacterDialog currentDialog;
    public List<string> alreadyTriggered = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //reference and set text to blank
        dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
        dialogText.text = "";

        //reference PlayerController script
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        //only get json file if variable isn't null
        if (dialogJson != null)
        {
            //read json file and store in class
            characterDialogs = JsonUtility.FromJson<CharacterDialogs>(dialogJson.text);
        }

        //make this object inactive until needed
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        //if in dialog mode
        if (inDialogMode)
        {
            //write text
            if (writeText)
            {
                //skip dialog if return is hit/held
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //set full sentence (skip typerwritter effect)
                    dialogText.text = currentDialog.sentence;

                    //reset variables
                    writeText = false;
                    stringIndex = 0;
                    timer = 0f;
                }
                //timer for printing text
                timer += Time.deltaTime;

                //print the next letter once timer hits print rate
                if (timer >= textPrintRate && stringIndex < currentDialog.sentence.Length + 1)
                {
                    //print next letter of sentence
                    dialogText.text = currentDialog.sentence.Substring(0, stringIndex);

                    //increment index and reset timer
                    stringIndex += 1;
                    timer = 0f;
                }
                //stop printing text
                else if (stringIndex >= currentDialog.sentence.Length + 1)
                {
                    //reset variables
                    writeText = false;
                    stringIndex = 0;
                    timer = 0f;
                }
            }
            //if not writing sentence and return is hit/held
            else if (writeText== false && Input.GetKeyDown(KeyCode.Return))
            {
                //blank text
                dialogText.text = "";

                //still dialog
                if (dialogsToPrint.Count > 0)
                {
                    //Debug.Log("DialogSystem: More dialogs");
                    //set sentence and remove from list
                    currentDialog = dialogsToPrint[0];
                    dialogsToPrint.RemoveAt(0);
                    writeText = true;
                }
                //no more dialog
                else
                {
                    //Debug.Log("DialogSystem: No more dialogs");
                    //re-enabled PlayerController and disable dialog gameobject
                    inDialogMode = false;
                    player.enabled = true;
                    this.gameObject.SetActive(false);
                    
                    //allow enemies to moving and abilities to ticking down again
                    ResumeGame();
                }
            }
        }
    }
    private void GetDialogToPrint(string trigger)
    {
        //check all dialogs for trigger
        foreach (CharacterDialog dialog in characterDialogs.dialog)
        {
            //if dialog's trigger matches trigger for dialog then add to list of dialogs to print
            if(dialog.trigger.CompareTo(trigger) == 0)
            {
                dialogsToPrint.Add(dialog);
            }
        }
    }
    public void EnableDialog(string trigger)
    {
        //only enable writing text if not already enabled
        if (writeText == false)
        {
            //check dialog wasn't triggererd before (prevent repeated dialog)
            if (alreadyTriggered.Contains(trigger) == false)
            {
                //prevent enemies from moving and abilities from ticking down
                PauseGame();

                //add trigger to list of already done triggers
                alreadyTriggered.Add(trigger);

                //disable to prevent player from moving (WARNING - player stay in air if activated[aka gravity is disabled])
                player.enabled = false;
                this.gameObject.SetActive(true);

                //get dialogs for trigger
                GetDialogToPrint(trigger);

                //set initial sentence and remove from list
                currentDialog = dialogsToPrint[0];
                dialogsToPrint.RemoveAt(0);

                //indicate dialog is active
                inDialogMode = true;

                //start writing first sentence
                writeText = true;
            }
        }
    }
    public void PauseGame()
    {
        //stop/freeze all enemies
        GameObject[] dustBalls = GameObject.FindGameObjectsWithTag("Dust");
        foreach (GameObject dust in dustBalls)
        {
            try
            {
                dust.GetComponent<lerper>().SetMovement(false);
            }
            catch (Exception e)
            {
                print(e);
            }
        }
        //prevent strong cheese from ticking down
        GameObject.Find("Player").GetComponent<Character_Movement>().SetInDialog(false);
    }
    public void ResumeGame()
    {
        //resume/unfreeze all enemies
        GameObject[] dustBalls = GameObject.FindGameObjectsWithTag("Dust");
        foreach (GameObject dust in dustBalls)
        {
            try
            {
                dust.GetComponent<lerper>().SetMovement(true);
            }
            catch (Exception e)
            {
                print(e);
            }
        }
        //allow strong cheese to ticking down
        GameObject.Find("Player").GetComponent<Character_Movement>().SetInDialog(true);
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



