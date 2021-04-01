using UnityEngine;

public class DialogSystemDetection : MonoBehaviour
{
    public DialogSystem dialogSystem = null;

    private void Awake()
    {
        dialogSystem = GameObject.Find("Dialog").GetComponent<DialogSystem>();
    }
    public void OnTriggerEnter(Collider other)
    {    
        //once player hits enable dialog
        if(other.gameObject.name.CompareTo("Mousy") == 0)
        {
            //send name of trigger to dialog script
            dialogSystem.EnableDialog(this.gameObject.name);

            //destroy objefct to prevent dialog re-triggering
            Destroy(this.gameObject);
        }
    }
}
