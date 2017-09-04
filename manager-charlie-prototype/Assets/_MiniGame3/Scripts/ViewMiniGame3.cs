using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;


namespace MiniGame3
{
    public class ViewMiniGame3 : MonoBehaviour
    {
        public GameObject InstChangingRoomPanel = null;             // 미니겜 패널
        public GameObject InstCoinShopPanel = null;                 // 코인샵 패널

        public Button PFClothBtn = null;                            // 버튼 프리팹
        public GameObject PFPagePanel = null;                       // 페이지 프리팹

        public List<Button> PFClothBtnList = null;                  // 버튼 프리팹 리스트
        public List<GameObject> PFPagePanelList = null;             // 페이지 프리팹 리스트

        private SceneMiniGame3 mScene = null;
        private int mCoin;                                          // 소지 코인


        private ClothItem[] mClothImg = new ClothItem[4];
        public List<ClothItem> ClothList = new List<ClothItem>();

        private ClothButtonData[] mClothBtn = new ClothButtonData[10];
        public List<ClothButtonData> ClothBtnList = new List<ClothButtonData>();
        
        private Vector2[] mButtonPos = new Vector2[4];

        void Awake()
        {
            SetChangingRoomBtnPos();
            GetItemImg();
            CreatePanel();
            CreateButtons();

            this.GetComponent<ViewChangingRoom>().SetPage(PFPagePanelList);            
            InstCoinShopPanel.GetComponent<ViewCoinShop>().SetClothList(ClothList);
        }
        
        public void SetScene(SceneMiniGame3 scene)
        {
            mScene = scene;
        }
        
        // 버튼 패널 생성
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
        // 버튼 생성
        public void CreateButtons()
        {            
            for (int i = 0; i < ClothBtnList.Count; i++)
            {
                var tempBtn = Instantiate(PFClothBtn, Vector2.zero, Quaternion.identity, PFPagePanelList[i / 4].transform);
                tempBtn.transform.localPosition = mButtonPos[i % 4];
                PFClothBtnList.Add(tempBtn);
            }

            for (int ti = 0; ti < PFClothBtnList.Count; ti++)
            {
                //PFItemBtnList[ti].onClick.AddListener(() => OnBtnItem(ImageList[ti].GetId()));
            }
        }

        // Cloth 정보 초기화
        public void InitClothBtn()
        {            
            for (int ti = 0; ti < mClothBtn.Length; ti++)
            {
                mClothBtn[ti] = new ClothButtonData();
                mClothBtn[ti].SetId(ti);
                ClothBtnList.Add(mClothBtn[ti]);
            }
        }

        // 메인창 버튼 기능
        private void OnBtnItem(int ti)
        {
            CDebug.LogFormat("Item ID: {0}", ClothList[ti].GetId());
            if (true == ClothList[ti].isWearing)
            {
                CDebug.Log("코스튬 해제");
                if (PFClothBtnList[ti])
                {
                    Instantiate(PFClothBtnList[ti], ClothList[ti].posImg, Quaternion.identity);
                }
            }
            else if(false == ClothList[ti].isWearing)
            {
                CDebug.Log("코스튬 장착");
                if (PFClothBtnList[ti])
                {
                    Instantiate(PFClothBtnList[ti], ClothList[ti].posImg, Quaternion.identity);
                }
            }
        }
        // 코인샵 버튼 기능
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

        public void GetItemImg()
        {            
            // 아이템 정보 초기화
            for (int ti = 0; ti < mClothImg.Length; ti++)
            {
                mClothImg[ti] = new ClothItem();
                mClothImg[ti].SetId(ti);                
                mClothImg[ti].SetPrice(ti * 100);                                
                ClothList.Add(mClothImg[ti]);
            }

            // 이미지 생성 위치 저장
            mClothImg[0].posImg = new Vector2(-140, 60);     // 모자
            mClothImg[1].posImg = new Vector2(-135, 20);     // 안경
            mClothImg[2].posImg = new Vector2(-140, -100);   // 옷
            mClothImg[3].posImg = new Vector2(0, 0);         // none
        }

        public void SetChangingRoomBtnPos()
        {
            mButtonPos[0] = new Vector2(0, 65);
            mButtonPos[1] = new Vector2(100, 65);
            mButtonPos[2] = new Vector2(0, -65);
            mButtonPos[3] = new Vector2(100, -65);
        }

        public void OnClickGoCoinShop()
        {
            InstChangingRoomPanel.SetActive(false);
            InstCoinShopPanel.SetActive(true);
            InstCoinShopPanel.GetComponent<ViewCoinShop>().SetClothList(ClothList);
        }
        public void OnClickGoChangingRoom()
        {
            InstChangingRoomPanel.SetActive(true);
            InstCoinShopPanel.SetActive(false);
        }    
    }
}
