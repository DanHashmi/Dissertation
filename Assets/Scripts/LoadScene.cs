using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace fishtanksoftware
{
public class LoadScene : MonoBehaviour
{
    public Dropdown dropdown;
    [SerializeField]
    private InputField input;
   
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void ConditionalSceneLoader(int sceneindex)
    {
        if(dropdown.value == 0 || input.text != ":)")
        {
            Debug.Log("Please select aquarium!");
        }

        else
        {
            {
                SceneManager.LoadScene(sceneindex);
            }
        }
    }
  
}
}