using UnityEngine;

public class LightToggleBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject uvLight, flashLight;

    private bool isOn = false;

    void Start()
    {
        uvLight.SetActive(false);
        flashLight.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        isOn = !isOn;
        uvLight.SetActive(isOn);
        flashLight.SetActive(!isOn);
    }
}
