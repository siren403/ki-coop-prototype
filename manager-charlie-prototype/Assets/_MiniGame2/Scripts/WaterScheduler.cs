using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class WaterScheduler : NutrientScheduler
    {
        public UIMiniGame2 UImini2 = null;

        public override void InitNutrient()
        {
            base.InitNutrient();
            UImini2.InActiveUILackWater(PotNumber);
        }

        public override void NormalNutrient()
        {
            base.NormalNutrient();
            UImini2.InActiveUILackWater(PotNumber);
        }

        public override void LackNutrient()
        {
            UImini2.ActiveUILackWater(PotNumber);
        }


        public override void ShowTextNutrientInfo(int currentAmount, int level)
        {
            UImini2.ShowTextWaterInfo(NowNutrientState, Timer, currentAmount, level, PotNumber);
        }


        public override void DeadFlower()
        {
            base.DeadFlower();
            UImini2.InActiveUILackNutrients(PotNumber);
        }
    }
}
