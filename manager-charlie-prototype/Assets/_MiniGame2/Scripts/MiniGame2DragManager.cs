using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CustomDebug;

namespace MiniGame2
{
    public class MiniGame2DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsWarter;
        public bool IsFertilizer;

        /**
         @fn    void Start()
        
         @brief 드래그 기능 있을 시 사용 -> 필요없으면 삭제
        
         @author    JT & YT
         @date  2017-09-05
         */
        void Start()
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsWarter == true)
            {

                //* 터치가 끝나면 위치값을 받아온다*/
                //* 그 위치 값이 좌표 내에 있으면 물뿌리개 시전*/
                if (transform.localPosition.x > -30 && transform.localPosition.x < 30 && transform.localPosition.y > -30 && transform.localPosition.y < 30)
                {
                    CDebug.Log("1번 화분에 물을 주었다");
                }
                else if (transform.localPosition.x > 100 && transform.localPosition.x < 175 && transform.localPosition.y > -30 && transform.localPosition.y < 30)
                {
                    CDebug.Log("2번 화분에 물을 주었다");
                }
                else if (transform.localPosition.x > -30 && transform.localPosition.x < 30 && transform.localPosition.y > -175 && transform.localPosition.y < -100)
                {
                    CDebug.Log("3번 화분에 물을 주었다");
                }
                else if (transform.localPosition.x > 100 && transform.localPosition.x < 175 && transform.localPosition.y > -175 && transform.localPosition.y < -100)
                {
                    CDebug.Log("4번 화분에 물을 주었다");
                }

                transform.localPosition = new Vector3(-150, 0, 0);

            }
            else if (IsFertilizer == true)
            {
                //* 터치가 끝나면 위치값을 받아온다*/
                //* 그 위치 값이 좌표 내에 있으면 물뿌리개 시전*/
                if (transform.localPosition.x > -30 && transform.localPosition.x < 30 && transform.localPosition.y > -30 && transform.localPosition.y < 30)
                {
                    CDebug.Log("1번 화분에 비료를 주었다");
                }
                else if (transform.localPosition.x > 100 && transform.localPosition.x < 175 && transform.localPosition.y > -30 && transform.localPosition.y < 30)
                {
                    CDebug.Log("2번 화분에 비료를 주었다");
                }
                else if (transform.localPosition.x > -30 && transform.localPosition.x < 30 && transform.localPosition.y > -175 && transform.localPosition.y < -100)
                {
                    CDebug.Log("3번 화분에 비료를 주었다");
                }
                else if (transform.localPosition.x > 100 && transform.localPosition.x < 175 && transform.localPosition.y > -175 && transform.localPosition.y < -100)
                {
                    CDebug.Log("4번 화분에 비료를 주었다");
                }

                transform.localPosition = new Vector3(-150, -120, 0);

            }
            CDebug.Log(transform.localPosition);

        }

    }
}
