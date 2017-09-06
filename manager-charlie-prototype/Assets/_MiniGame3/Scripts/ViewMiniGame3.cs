using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;


namespace MiniGame3
{
    /**
     @class ViewMiniGame3
    
     @brief mini game 3의 view
    
     @author    Kyoungil
     @date  2017-09-05
     */

    public class ViewMiniGame3 : MonoBehaviour
    {
        public GameObject InstChangingRoomPanel = null;             // 미니겜 패널
        public GameObject InstCoinShopPanel = null;                 // 코인샵 패널

        public ClothButtonData PFClothBtn = null;                   // 버튼 프리팹
        public GameObject PFPagePanel = null;                       // 페이지 프리팹

        public List<ClothButtonData> PFClothBtnList = null;         // 버튼 프리팹 리스트
        public List<GameObject> PFPagePanelList = null;             // 페이지 프리팹 리스트

        private SceneMiniGame3 mScene = null;
        private int mCoin;                                          // 소지 코인
        
        private ClothItem[] mClothImg = new ClothItem[10];          // 옷 데이터 전부를 넣을 배열(임시로 숫자 넣음)
        public List<ClothItem> ClothList = new List<ClothItem>();   

        private ClothButtonData[] mClothBtn = new ClothButtonData[10]; 
        public List<ClothButtonData> ClothBtnList = new List<ClothButtonData>();
        
        private Vector2[] mButtonPos = new Vector2[4];              // ChangingRoom 버튼 좌표

        void Awake()
        {
            GetItemData();                   
            SetChangingRoomBtnPos();
            ClothBtnSetData();
            CreatePanel();
            CreateButtons();

            this.GetComponent<ViewChangingRoom>().SetPage(PFPagePanelList);            
            InstCoinShopPanel.GetComponent<ViewCoinShop>().SetClothList(ClothList);
            
        }
        
        public void SetScene(SceneMiniGame3 scene)
        {
            mScene = scene;
        }

        /**
         @fn    public void GetItemData()
        
         @brief 옷 아이템 데이터
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void GetItemData()
        {
            // 아이템 정보 가져옴
            for (int ti = 0; ti < mClothImg.Length; ti++)
            {
                mClothImg[ti] = new ClothItem();
                mClothImg[ti].SetId(ti);                        // id    저장
                mClothImg[ti].SetPrice(ti * 100);               // price 저장
                ClothList.Add(mClothImg[ti]);                   // List에 저장
            }
        }

        /**
         @fn    public void SetChangingRoomBtnPos()
        
         @brief ChangingRoom 버튼 위치 저장
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void SetChangingRoomBtnPos()
        {
            mButtonPos[0] = new Vector2(0, 65);
            mButtonPos[1] = new Vector2(100, 65);
            mButtonPos[2] = new Vector2(0, -65);
            mButtonPos[3] = new Vector2(100, -65);
        }

        /**
         @fn    public void ClothBtnSetData()
        
         @brief Cloth 버튼 데이터 초기화
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void ClothBtnSetData()
        {
            for (int ti = 0; ti < mClothBtn.Length; ti++)
            {
                mClothBtn[ti] = new ClothButtonData { ID = ti };
                ClothBtnList.Add(mClothBtn[ti]);                // 동적 할당 후 List에 저장
            }
        }

        /**
         @fn    public void CreatePanel()
        
         @brief ChangingRoom 버튼 패널 생성
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void CreatePanel()
        {
            int tIndex = 0;
            if (0 == ClothBtnList.Count % 4)
            {
                tIndex = ClothBtnList.Count / 4;
            }
            else
            {
                tIndex = (ClothBtnList.Count / 4) + 1;
            }

            for (int j = 0; j < tIndex; j++)
            {
                Vector2 tPos = Vector2.zero;
                var tempPanel = Instantiate(PFPagePanel, tPos, Quaternion.identity, InstChangingRoomPanel.transform);
                tempPanel.transform.localPosition = new Vector2(j * 400, 0);
                PFPagePanelList.Add(tempPanel);
            }

        }

        /**
         @fn    public void CreateButtons()
        
         @brief ChangingRoom 버튼 생성
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void CreateButtons()
        {            
            for (int i = 0; i < ClothList.Count; i++)
            {
                var tempBtn = Instantiate(PFClothBtn, Vector2.zero, Quaternion.identity, PFPagePanelList[i / 4].transform);
                tempBtn.ID = ClothList[i].GetId();
                tempBtn.SetPrice(ClothList[i].GetPrice());
                tempBtn.transform.localPosition = mButtonPos[i % 4];
                tempBtn.GetComponent<Button>().onClick.AddListener(() => OnBtnItem(tempBtn.ID));
                PFClothBtnList.Add(tempBtn);
            }                        
        }

        /**
         @fn    private void OnBtnItem(int tIndex)
        
         @brief 메인창 버튼 기능.
        
         @author    Kyoungil
         @date  2017-09-05
        
         @param tIndex  Zero-based index of the.
         */
        private void OnBtnItem(int tIndex)
        {
            CDebug.LogFormat("Cloth ID: {0}", PFClothBtnList[tIndex].ID);

            if (true == ClothList[tIndex].isWearing)
            {
                CDebug.Log("코스튬 해제");
                if (PFClothBtnList[tIndex])
                {
                    Instantiate(PFClothBtnList[tIndex], ClothList[tIndex].posImg, Quaternion.identity);
                }
            }
            else if (false == ClothList[tIndex].isWearing)
            {
                CDebug.Log("코스튬 장착");
                if (PFClothBtnList[tIndex])
                {
                    Instantiate(PFClothBtnList[tIndex], ClothList[tIndex].posImg, Quaternion.identity);
                }
            }
        }

        /**
         @fn    private void OnBtnCoinShopItem(int ti)
        
         @brief 코인샵 버튼 기능
        
         @author    Kyoungil
         @date  2017-09-05
        
         @param ti  The ti.
         */
        private void OnBtnCoinShopItem(int ti)
        {
            // 코인 체크 && 구매 체크
            if (ClothList[ti].GetPrice() > mCoin)
            {
                Instantiate(PFClothBtnList[ti], ClothList[ti].posImg, Quaternion.identity);
                ClothList[ti].isWearing = true;
                mCoin -= ClothList[ti].GetPrice();
                CDebug.Log("구매완료");
            }
            else if(true == ClothList[ti].isBought)
            {
                CDebug.Log("이미 구매함");
            }
        }

        /**
         @fn    public void OnClickGoCoinShop()
        
         @brief CoinShop으로 이동 기능
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void OnClickGoCoinShop()
        {
            InstChangingRoomPanel.SetActive(false);
            InstCoinShopPanel.SetActive(true);
            InstCoinShopPanel.GetComponent<ViewCoinShop>().SetClothList(ClothList);
        }

        /**
         @fn    public void OnClickGoChangingRoom()
        
         @brief ChaningRoom으로 이동 기능
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void OnClickGoChangingRoom()
        {
            InstChangingRoomPanel.SetActive(true);
            InstCoinShopPanel.SetActive(false);
        }    
    }
}
