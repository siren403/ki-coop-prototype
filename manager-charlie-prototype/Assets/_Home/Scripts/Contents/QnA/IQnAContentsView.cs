using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.QnA
{
    public interface IQnAContentsView
    {
        void ShowEpisode();
        void ShowQuestion();
        void ShowSituation();
        void ShowAnswer();
        void SelectAnswer();
        void CorrectAnswer();
        void WrongAnswer();
        void ShowReward();
        void ClearEpisode();
    }
}
