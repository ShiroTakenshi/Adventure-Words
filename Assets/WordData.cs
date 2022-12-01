using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{

    [SerializeField] private TMP_Text charText;
    [HideInInspector] public char CharValue;
    private Button objectButton;

    private void Awake() {
        objectButton = GetComponent<Button>();
        if(objectButton){
            objectButton.onClick.AddListener(()=>CharSelect());
        }
    }

    public void SetChar(char value){
        charText.text = value + "";
        CharValue = value;
    }

    private void CharSelect() {
        GameManager.instance.SelectedOptions(this);
    }
}
