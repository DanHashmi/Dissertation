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
        System.IO.File.WriteAllText(@"Assets\Scripts\CSVTextFile.txt", string.Empty);
    }

    public void ConditionalSceneLoader(int sceneindex)
    {
        if(dropdown.value == 0)
        {
            Debug.Log("Please select aquarium!");
        }
        else if(input.text != ":)")
        {
            Debug.Log("Plese enter aquarium capacity!");
        }
        else
        {
            {
                SceneManager.LoadScene(sceneindex);
            }
        }
    }
    public void FinalSceneLoader(int Sceneindex2)
    {
        if(PlayerPrefs.GetInt("SumCheck")==1)
        {
            SceneManager.LoadScene(Sceneindex2);
            System.IO.File.WriteAllText(@"Assets\Scripts\CSVTextFile.txt", string.Empty);
        }

        else
        {
            Debug.Log("Aquarium Overcrowded! Please change number of Fish!");
        }
    }    
}
}