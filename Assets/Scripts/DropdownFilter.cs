using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fishtanksoftware
{
public class DropdownFilter : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
 
    [SerializeField]
    private Dropdown dropdown;
     
    private List<Dropdown.OptionData> dropdownOptions;

    void Start()
    {
        dropdownOptions = dropdown.options;
    }    
    public void FilterDropdown( string input ) //issue where dropdown isnt selected
    {
        dropdown.options = dropdownOptions.FindAll( option => option.text.IndexOf( input ) >= 0 );
        List<Dropdown.OptionData> list = dropdown.options;
        if(list.Count == 0)
        {
            int filtercheck = 1;
            PlayerPrefs.SetInt("FilterCheck", filtercheck);
        }
        else
        {
            int filtercheck = 0;
            PlayerPrefs.SetInt("FilterCheck", filtercheck);
        }
    }

}
}