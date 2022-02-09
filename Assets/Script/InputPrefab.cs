using TMPro;
using UnityEngine;


public class InputPrefab : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    public TMP_InputField InputField => _inputField;
    [SerializeField] private TMP_Text _placeholder;


    public void Initialized(string placeholder)
    {
        _placeholder.text = placeholder;
    }

    public void SetInput(string input)
    {
        _inputField.text = input;
    }
}
