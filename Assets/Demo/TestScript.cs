using System.Collections;
using System.Collections.Generic;
using AudioEngine;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioEngine.AudioEngine.Instance.PlaySound("Lounge Game1", Vector3.zero, 0.5f);
        }
    }
}