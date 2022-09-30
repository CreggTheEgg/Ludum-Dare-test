using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
