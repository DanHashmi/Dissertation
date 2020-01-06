using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.Examples
{
    public class ComboBoxChanged : MonoBehaviour
    {
        List<string> names = new List<string>() {"Please Select Aquarium","Coldwater", "Temperate", "Tropical"};

        public void ComboBoxChangedEvent(string text)
        {

            Debug.Log("ComboBox changed [" + text + "]");
        }

        public void AutoCompleteComboBoxChangedEvent(string text)
        {
            Debug.Log("AutoCompleteComboBox changed [" + text + "]");
        }

        public void AutoCompleteComboBoxSelectionChangedEven(string text, bool valid)
        {
            // AutoCompleteComboBox.AddItems(names);
            Debug.Log("AutoCompleteComboBox selection changed [" + text + "] and its validity was [" + valid + "]");
        }

        public void DropDownChangedEvent(int newValue)
        {

            Debug.Log("DropDown changed [" + newValue + "]");
        }
    }
}