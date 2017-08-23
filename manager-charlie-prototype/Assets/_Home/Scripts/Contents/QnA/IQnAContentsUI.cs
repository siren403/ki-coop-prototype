using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents
{
    public interface IQnAContentsUI
    {
        void ShowEpisode();
        void ShowQuestion();
        void ShowSituation();
        void ShowAnswer();
        void SelectAnswer();
        void ShowReward();
        void ClearEpisode();
    }
}
