using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class FertilizerScheduler : NutrientScheduler
    {

        // Use this for initialization
        void Start()
        {

        }

        public override void LackNutrient()
        {
            mFlower.LackFertilizer();
        }

    }

}
