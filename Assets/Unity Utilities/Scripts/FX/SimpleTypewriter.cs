using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[System.Serializable]

public class SimpleTypewriter : MonoBehaviour {


	bool typing=false;
	string textToTypewrite;
	public Text text;

	void OnEnable () {
		InitText();

	}

	public void InitText(){
		if (!text)
			text = GetComponent<Text> ();
		textToTypewrite = text.text;
		text.text = "";
		typing = true;
		index = 0;
	}

	void OnDisable(){
		text.text = textToTypewrite;
		typing = false;
	}


	public float t=0;
	int index=0;
	public float charInterval  = 0.04f;
	bool waitedExtraSecondsThisCharacter = false;
	int waitedExtraSecondsCounter = 0;
	void Update(){
		if (typing){
			t -= Time.deltaTime;
            int charsThisFrame = 3;
			if (t < 0 && index < textToTypewrite.Length){
                for (int i = 0; i < charsThisFrame;i++){
                    if (!typing) continue;

                    t = charInterval;
                    if (waitedExtraSecondsThisCharacter){
                        waitedExtraSecondsCounter ++;
                        if (waitedExtraSecondsCounter > 5){
                            waitedExtraSecondsThisCharacter = false;
                            waitedExtraSecondsCounter = 0;
                            
                        }
                    }
                    if (index > 5){
                        
                        if ( 
                            (
                                ( textToTypewrite[index] == '.' ) // after dot
                                || textToTypewrite[index] == '!'  // after any !
                                || textToTypewrite[index] == '?')  // after any ?
                            && !waitedExtraSecondsThisCharacter) { // but not twice in a row within 5 seconds
                            
                            t += 1.5f; // add a delay for periods, exclanation marks and quetions.
                            waitedExtraSecondsThisCharacter = true;
                        }
                    }
                    
                    
                    text.text += textToTypewrite[index];
                    index++;
                    if (index == textToTypewrite.Length) typing = false;
                }
			} 
		}
	}



}
