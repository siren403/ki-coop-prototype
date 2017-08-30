using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QnaContents2Data
{
    public int QuestionId;
    public int EpisodeId;

    public string Question;
    public string Correct;
    public string Wrong;
    public string ObjectState;



    /**id = 문제 id ,  foodID = 음식 id , name = 음식 이름 , used  = 이전에 제출 되었던 문제 이면 true로 변경해준다 */

    /**전체적 흐름 */
    /** 1. 문제 설정 : Qna 에서 랜덤으로 하나 뽑음 -> used 가 true 인것을 제외하고 문제 한개를 뽑는다. */
    /** 2. 정답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true로 변경하여 이전에 제출되었다는 것을 체크 해준다. */
    /** 3. 오답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true가 아니고 현재 문제 코드 (A=1, B=2, C=3 ,...)가 아닌 것들 세개 설정 -> */

    public QnaContents2Data(int id, int episode, string question, string correct, string wrong, string objectstate)
    {
        QuestionId = id;
        EpisodeId = episode;
        Question = question;
        Correct = correct;
        Wrong = wrong;
        ObjectState = objectstate;
    }
}
