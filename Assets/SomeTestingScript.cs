using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class SomeTestingScript : MonoBehaviour
    {
        public int a = 2;
        public float z = 0.1f;
        public string we = "we";
        public bool whatever = true;
        private int _b = 1;
        protected int C = 3;

        public void Public(int a)
        {
            Debug.Log("Public function : "+a);
        }

        public int HeheheSomeInt()
        {
            return 10;
        }
        public int HeheheSomeInt(int someVariable,float dff,Vector3 wae)
        {
            return someVariable+1;
        }

        public Vector3 IntToVector(int inputInt)
        {
            return Vector3.one * a;
        }
        private void Private(int a)
        {
            Debug.Log("Private function : "+a);
        }

        private protected void PrivateProtected(int a)
        {
            Debug.Log("Private prot function : "+a);
        }
    }
}