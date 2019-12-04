using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private Transform messageParentPanel;
    [SerializeField]
    private GameObject MessagePrefab;
    private string message = "";

    public void SetMessage(string message)
    {
        this.message = message;
    }

    
    public void ShowMessage()
    {
        GameObject clone = (GameObject) Instantiate (MessagePrefab);
        clone.transform.SetParent (messageParentPanel);
        clone.transform.SetSiblingIndex (messageParentPanel.childCount - 2);
        clone.GetComponent<MessageFunction>().ShowMessage (message);
    }
}
