using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace Util.Inspector
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class InspectorButton : Editor
    {
        //인스펙터에서 실행가능한 메소드를 인자가 없는 메소드로 한정해놓았기 때문에
        //Invoke실행 인자가 없다고 넘길 값을 공용으로 쓰기위해 스태틱으로 선언
        private static object[] EmptyParamList = new object[0];

        private List<MethodInfo> mMethodList = null;
        private Object mTargetObject = null;


        private void OnEnable()
        {
            Init(this.target);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawButtons();
        }
        /// <summary>
        /// 버튼속성이 붙어있는 오브젝트의 모든 메소드정보를 수집하여 저장
        /// </summary>
        /// <param name="targetObject"></param>
        public void Init(Object targetObject)
        {
            this.mTargetObject = targetObject;
            mMethodList = mTargetObject.GetType()//targetObject의 타입을 받아옴
                                                 //해당 타입에 정의되어있는 메소드 중 public,private로 선언된 인스턴스 메소드의 정보를 받아옴
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(method =>
                {
                    //위에서 받은 메소드들 중 선언되어 있는 ButtonAttribute들은 가져옴
                    var attrs = method.GetCustomAttributes(typeof(ButtonAttribute), false);

                    //파라매터가 없고,제네릭 파라매터가 없으면 수집
                    if (attrs.Length == 1 &&
                    method.GetParameters().Length == 0 &&
                    method.ContainsGenericParameters == false)
                    {
                        return true;
                    }

                    return false;
                })
                .ToList();//결과를 List로 변환
        }

        //OnInspectorGUi에서 버튼을 그리기 위해 호출
        public void DrawButtons()
        {
            if (mMethodList.Count > 0)
            {
                EditorGUILayout.HelpBox("ExecuteMethods", MessageType.None);
                ShowMethodButtons();
            }
        }
        private void ShowMethodButtons()
        {
            foreach (MethodInfo method in mMethodList)
            {
                //ObjectNames.NicifyVariableName();
                //Reference : https://docs.unity3d.com/kr/current/ScriptReference/ObjectNames.NicifyVariableName.html
                string methodName = ObjectNames.NicifyVariableName(method.Name);
                if (GUILayout.Button(methodName))
                {
                    method.Invoke(mTargetObject, EmptyParamList);
                }
            }
        }
    }
}