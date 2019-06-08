using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour {

    public GameObject worksheetDisplay;
    public GameObject interfaceDisplay;
    GameObject buttonText;
    char buttonCharacter;
    string buttonString;
    string spriteName;
    Sprite spr;
    bool shape = false;

    void Setup(){
        //remove button text
        buttonText = transform.Find("Text").gameObject;
        buttonText.GetComponent<Text>().text = "";
        buttonCharacter = ' ';
        buttonString = null;
    }

    //set button to a character
    public void setButtonCharacter(char c)
    {
        Setup();
        buttonCharacter = c;
    }

    //get button character
    public char getButtonCharacter()
    {
        return buttonCharacter;
    }

    //set button to a string
    public void setButtonString(string s)
    {
        Setup();
        buttonString = s;
    }

    //get button string
    public string getButtonString()
    {
        return buttonString;
    }

    //set button to a shape
    public void setShape(){
        shape = true;
    }

    //get shape of button
    public bool getShape(){
        return shape;
    }

    //set sprite (character)
    public void setCharacter(char c){
        Setup();
        setButtonCharacter(c);
        spriteName = "Sprites/Keyboard/keys";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spriteName);
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == ("keys_" + c)){
                spr = sprites[i];
                this.GetComponent<Image>().sprite = spr;   
            }
                
        }

    }

    //set sprite(number)
    public void setNumber(int no)
    {
        Setup();
        spriteName = "Sprites/Keyboard/keys";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spriteName);
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == ("keys_0" + no))
            {
                spr = sprites[i];
                this.GetComponent<Image>().sprite = spr;
            }

        }

    }

    //set sprite(big character)
    public void setBigCharacter(string s)
    {
        Setup();
        setButtonString(s);
        spriteName = "Sprites/Keyboard/bigKeys";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spriteName);
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == ("bigKeys_" + s))
            {
                spr = sprites[i];
                this.GetComponent<Image>().sprite = spr;
            }

        }

    }

    //set sprite (shape)
    public void setShapeCharacters(char c){
        Setup();
        setButtonCharacter(c);
        setShape();
        //update sprite for button
        spriteName = "Sprites/Keyboard/shapes";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spriteName);
        for (int j = 0; j < sprites.Length; j++)
        {
            if (sprites[j].name == ("shapes_" + c))
            {
                GetComponent<Image>().sprite = sprites[j];
                break;
            }

        }
    }

    //add a letter
    public void AddLetter()
    {
        worksheetDisplay.GetComponent<WorksheetDisplayScript>().AddLetter(buttonCharacter);
    }

    //remove a letter
    public void DelLetter()
    {
        worksheetDisplay.GetComponent<WorksheetDisplayScript>().DelLetter();
    }

    //set button as selected
    public void SelectButton(){
        GameObject currentButton = gameObject;
        interfaceDisplay.GetComponent<InterfaceDisplayScript>().SelectButton(gameObject);
    }

}
