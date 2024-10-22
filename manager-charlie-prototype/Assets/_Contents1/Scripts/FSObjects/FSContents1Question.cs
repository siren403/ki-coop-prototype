﻿using System.Collections;
using System.Collections.Generic;
using Contents.QnA;
using CustomDebug;
using Util;

namespace Contents1
{
    public class FSContents1Question : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Question;
            }
        }

        public override void Enter()
        {
            (Entity as SceneContents1).ResetWrongCount();
            Entity.View.ShowQuestion();
        }
       
    }
}

