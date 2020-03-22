using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioEngine
{
    public class Channel
    {
        public bool IsPlaying;
        public string EventName { get; set; }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }


        public void Setup(string audioSource, Vector3 pos, float volume)
        {
            throw new System.NotImplementedException();
        }
    }
}