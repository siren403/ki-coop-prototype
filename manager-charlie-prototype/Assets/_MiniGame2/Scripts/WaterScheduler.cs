using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class WaterScheduler : NutrientScheduler
    {
        // Use this for initialization
        void Start()
        {

        }

        public override void LackNutrient()
        {
            mFlower.LackWater();
        }

    }
}
