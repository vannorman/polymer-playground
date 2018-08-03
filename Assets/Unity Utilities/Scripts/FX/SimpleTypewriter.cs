using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[System.Serializable]

public class SimpleTypewriter : MonoBehaviour {


    public enum State { 
        Ready,
        Typing,
        TypingComplete,
        FadingOut
    }
    float completedTimer = 5f;
    public State state;
    public void SetState(State newState) {
        //Debug.Log("<b><color=#f70>State:" + newState + "</color></b>");
        state = newState;
    }
	
	string textToTypewrite;
	public Text text;

    int CharsPerLine {
        get {
            
            return Mathf.RoundToInt(text.GetComponent<RectTransform>().sizeDelta.x / text.fontSize * 2) - 4;
        }
    }

	void OnEnable () {
		//InitText();

	}

    string cachedToSay = "";
    bool waitingForStateReady = false;
	public void TypeText(string toSay = ""){
        completedTimer = 5;
        text.color = Color.white;

        t = 0;
		if (!text)
			text = GetComponent<Text> ();
        if (toSay == ""){
            textToTypewrite = text.text;
        } else {
            textToTypewrite = toSay;
        }
        textToTypewrite = Utils2.InsertLineBreaks(textToTypewrite, CharsPerLine);
		text.text = "";
		SetState(State.Typing);
		index = 0;
	}

	


	public float t=0;
	int index=0;
	public float charInterval  = 0.04f;
	bool waitedExtraSecondsThisCharacter = false;
    int waitedExtraSecondsCounter = 0;
    public int charsPerFrame = 2;

    //public delegate void OnStateReadyDelegate(string cached);
    //public OnStateReadyDelegate onStateReady;

    //public void OnStateReady(string c) {
    //    TypeText(c);
    //    onStateReady -= OnStateReady;
    //}


    void Update(){
        
        switch (state) { 
            case State.Ready:
                
                //if (onStateReady != null) {
                    //onStateReady(cachedToSay);

                    //waitingForStateReady = false;

                //}
                break;
            case State.Typing:

            t -= Time.deltaTime;
            if (t < 0 && index < textToTypewrite.Length)
            {
                for (int i = 0; i < charsPerFrame; i++)
                {
                    if (state != State.Typing) continue;

                    t = charInterval;
                    if (waitedExtraSecondsThisCharacter)
                    {
                        waitedExtraSecondsCounter++;
                        if (waitedExtraSecondsCounter > 5)
                        {
                            waitedExtraSecondsThisCharacter = false;
                            waitedExtraSecondsCounter = 0;

                        }
                    }
                    if (index > 5)
                    {

                        if (
                            (
                                (textToTypewrite[index] == '.') // after dot
                                || textToTypewrite[index] == '!'  // after any !
                                || textToTypewrite[index] == '?')  // after any ?
                            && !waitedExtraSecondsThisCharacter)
                        { // but not twice in a row within 5 seconds

                            t += 1.5f; // add a delay for periods, exclanation marks and quetions.
                            waitedExtraSecondsThisCharacter = true;
                        }
                    }


                    text.text += textToTypewrite[index];
                    index++;
                    if (index == textToTypewrite.Length)
                    {
                        SetState(State.TypingComplete);
                        break;

                        //SetState(State.FadingOut);
                    }
                }
            }
                break;
            case State.TypingComplete:
                completedTimer -= Time.deltaTime;
                if (completedTimer < 0) {
                    SetState(State.FadingOut);
                }
                break;
            case State.FadingOut:
                //float fadespeed = 3f;
                //text.color = Color.Lerp(text.color, new Color(255, 255, 255, 0), Time.deltaTime * fadespeed);
                //if (Mathf.Abs(text.color.a - 0) < .01f) {
                //    text.color = new Color(255, 255, 255, 0);
                //    SetState(State.Ready);
                //}

                break;
        

        
        }
	}


}
