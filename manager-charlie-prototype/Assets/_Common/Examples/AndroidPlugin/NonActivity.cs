using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AndroidPlugin;

namespace Examples
{
    public class NonActivity : AndroidObject
    {
        public NonActivity(string packageName, string className)
            : base(packageName, className)
        {

        }
        public string GetString(string a)
        {
            return this.CallMathod<string>("GetString", a);
        }
        public int GetInt(int a)
        {
            return this.CallMathod<int>("GetInt", a);
        }
    }
}
