using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CustomDebug;

namespace MiniGame2
{
    public class SceneMyGarden : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
    {
        //* 드래그 내용 */
        bool moving;

        //* 화분에 닿았을 경우 */
        public Transform NutrientGroup;

        private void Awake()
        {
            moving = false;
        }


        //* 드래그 시작 */
        public void OnBeginDrag(PointerEventData eventData)
        {
            moving = true;
        }

        //* 드래그 중 */
        public void OnDrag(PointerEventData eventData)
        {
            this.transform.position = eventData.position;
        }

        //* 드래그 끝났을 때 
        public void OnEndDrag(PointerEventData eventData)
        {

        }
        //* 화분에 닿았을 때 */
        public void OnPointerEnter(PointerEventData eventData)
        {
            moving = false;
        }
    }
}