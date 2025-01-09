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