using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn;
    public TMP_InputField inputField;
    public Toggle toggle;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnClkEvent()
    {
        print(inputField.text);
        print(toggle.isOn);

        if(toggle.isOn)
        {
            // �ð� �۵�
        }
        print(slider.value);
        if(slider.value > 0.5f)
        {
            // �ð��� �ӵ��� 50% ������ �����.
        }
    }
}
