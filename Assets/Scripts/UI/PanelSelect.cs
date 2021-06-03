using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{

    [System.Serializable]
    public struct PanelInfo
    {
        public GameObject panel;
        public Button button;
        public KeyCode keycode;

    }

    public KeyCode toggleKey;
    public GameObject masterPanel;

    public PanelInfo[] panelInfos;

    void Start()
    {
        foreach (PanelInfo pi in panelInfos)
        {
            pi.button.onClick.AddListener(delegate { ButtonEvent(pi); });
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            masterPanel.SetActive(!masterPanel.activeSelf);
        }

        foreach(PanelInfo pi in panelInfos)
        {
            if (Input.GetKeyDown(pi.keycode))
            {
                SetPanelActive(pi);
            }
        }
    }

    void SetPanelActive(PanelInfo pi)
    {
        for(int i = 0; i < panelInfos.Length; i++)
        {
            bool active = panelInfos[i].Equals(pi);
            panelInfos[i].panel.SetActive(active);
        }
    }

    void ButtonEvent(PanelInfo panelInfo)
    {
        SetPanelActive(panelInfo);
    }
}
