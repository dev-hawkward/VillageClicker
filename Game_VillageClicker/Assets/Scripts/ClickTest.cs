using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HW
{
    public class ClickTest : HWBehaviour
    {
        [SerializeField] private AudioSource clickSound;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            Debug.Log("Clicked!");
            clickSound.Play();
        }
    }
}
