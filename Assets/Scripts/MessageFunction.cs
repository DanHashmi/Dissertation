﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFunction : MonoBehaviour
{
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    public void ShowMessage(string message)
    {
        text.text = message;
    }
    
    public void HideMessage()
    {
        Destroy(gameObject);
    }

}
