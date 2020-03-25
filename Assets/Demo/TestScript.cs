using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioEngine.AudioEngine.Instance.PlaySound("Lounge Game1", Vector3.zero);
        }
    }
}