using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using Contents3;
using CustomDebug;



public class SituationDirecting : MonoBehaviour {


    private QnAContentsBase.State mState;
    private SceneContents3 mContents3 = null;



	void Start () {

        mContents3 = new SceneContents3();
        mState = mContents3.GetState();

        ShowSituation();
        
	}
	
	void Update () {
		
	}


    void ShowSituation()
    {
        if (QnAContentsBase.State.ShowSituation == mState)
        {
             // 상황연출 anim show
        CDebug.Log("ShowSituation");

        mState = QnAContentsBase.State.ShowQuestion;
        mContents3.ChangeState(mState);
        CDebug.Log(mState);

                        
        }
    }

}
