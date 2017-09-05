using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.QnA
{
    public interface IQnAView
    {
        /**
         @fn    void ShowEpisode();
        
         @brief 에피소드 리스트를 보여준다
         */
        void ShowEpisode();

        /**
         @fn    void ShowSituation();
        
         @brief 선택한 에피소드의 상황애니메이션을 보여준다
         */
        void ShowSituation();
        /**
         @fn    void ShowQuestion();
        
         @brief 문제를 보여준다
         */
        void ShowQuestion();

        /**
         @fn    void ShowAnswer();
        
         @brief 선택지를 보여준다
         */
        void ShowAnswer();

        /**
         @fn    void HurryUpAnswer();
        
         @brief 선택 대기시간 초과 시 연출
         */
        void HurryUpAnswer();

        /**
         @fn    void CorrectAnswer();
        
         @brief 정답 시의 View처리
         */
        void CorrectAnswer();

        /**
         @fn    void WrongAnswer();
        
         @brief 오답 시의 View처리
         */
        void WrongAnswer();

        /**
         @fn    void ShowReward();
        
         @brief 모든 문제를 진행 후 결과에 따라 보상 처리
         */
        void ShowReward();

        /**
         @fn    void ClearEpisode();
        
         @brief 보상 처리 후 클리어 연출, 이후에는 Outro UI로 진행
         */
        void ClearEpisode();

        /**
         @fn    void ShowOutro();
        
         @brief 에피소드 종료 후 분기 메뉴
         */
        void ShowOutro();
    }
}
