using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    public GameObject ControlsWindow;

    public KeyCode LeftFlipper;
    public KeyCode RightFlipper;
    public KeyCode Launcher;
    public KeyCode LeftTilt;
    public KeyCode RightTilt;

    private KeyCode TLF;
    private KeyCode TRF;
    private KeyCode TL;
    private KeyCode TLT;
    private KeyCode TRT;

    public Text lf;
    public Text rf;
    public Text l;
    public Text lt;
    public Text rt;

    private Keys preppedKey = Keys.None;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if(preppedKey != Keys.None && Input.anyKeyDown)
        {
            KeyCode pressedKey = KeyCode.None;
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    pressedKey = keyCode;
                    break;
                }
            }

            if (preppedKey == Keys.LFlipper)
                SetLeftFlipper(pressedKey);
            else if (preppedKey == Keys.RFlipper)
                SetRightFlipper(pressedKey);
            else if (preppedKey == Keys.Launch)
                SetLauncher(pressedKey);
            else if (preppedKey == Keys.LTilt)
                SetLeftTilt(pressedKey);
            else if (preppedKey == Keys.RTilt)
                SetRightTilt(pressedKey);

            SetButtonDefaultTexts();
            preppedKey = Keys.None;
        }
    }

    public void Init()
    {
        ControlsWindow.SetActive(true);
        SetButtonsInitial();
        TLF = LeftFlipper;
        TRF = RightFlipper;
        TL = Launcher;
        TLT = LeftTilt;
        TRT = RightTilt;
    }

    public void Back()
    {
        SetButtonsInitial();
        ControlsWindow.SetActive(false);
    }

    public void SaveButtonPressed()
    {
        LeftFlipper = TLF;
        RightFlipper = TRF;
        Launcher = TL;
        LeftTilt = TLT;
        RightTilt = TRT;
        SetButtonsInitial();
        ControlsWindow.SetActive(false);
    }


    //This is fuckin dumb
    public void PrepareSetLeftFlipper()
    {
        preppedKey = Keys.LFlipper;
        SetButtonDefaultTexts();
        lf.text = "Press a key to set Left Flipper";
    }
    public void PrepareSetRightFlipper()
    {
        preppedKey = Keys.RFlipper;
        SetButtonDefaultTexts();
        rf.text = "Press a key to set Right Flipper";
    }
    public void PrepareSetLauncher()
    {
        preppedKey = Keys.Launch;
        SetButtonDefaultTexts();
        l.text = "Press a key to set Launch";
    }
    public void PrepareSetLeftTilt()
    {
        preppedKey = Keys.LTilt;
        SetButtonDefaultTexts();
        lt.text = "Press a key to set Left Tilt";
    }
    public void PrepareSetRightTilt()
    {
        preppedKey = Keys.RTilt;
        SetButtonDefaultTexts();
        rt.text = "Press a key to set Right Tilt";
    }

    private void SetButtonDefaultTexts()
    {
        lf.text = TLF.ToString();
        rf.text = TRF.ToString();
        l.text = TL.ToString();
        lt.text = TLT.ToString();
        rt.text = TRT.ToString();
    }

    private void SetButtonsInitial()
    {
        lf.text = LeftFlipper.ToString();
        rf.text = RightFlipper.ToString();
        l.text = Launcher.ToString();
        lt.text = LeftTilt.ToString();
        rt.text = RightTilt.ToString();
    }

    private void SetLeftFlipper(KeyCode c)
    {
        TLF = c;
        //if (TRF == c)
        //    TRF = KeyCode.None;
        //else if(TL == c)
        //    TL = KeyCode.None;
        //else if (TLT == c)
        //    TLT = KeyCode.None;
        //else if (TRT == c)
        //    TRT = KeyCode.None;
    }
    private void SetRightFlipper(KeyCode c)
    {
        TRF = c;
        //if (TLF == c)
        //    TLF = KeyCode.None;
        //else if (TL == c)
        //    TL = KeyCode.None;
        //else if (TLT == c)
        //    TLT = KeyCode.None;
        //else if (TRT == c)
        //    TRT = KeyCode.None;
    }
    private void SetLauncher(KeyCode c)
    {
        TL = c;
        //if (TLF == c)
        //    TLF = KeyCode.None;
        //else if (TRF == c)
        //    TRF = KeyCode.None;
        //else if (TLT == c)
        //    TLT = KeyCode.None;
        //else if (TRT == c)
        //    TRT = KeyCode.None;
    }
    private void SetLeftTilt(KeyCode c)
    {
        TLT = c;
        //if (TLF == c)
        //    TLF = KeyCode.None;
        //else if (TRF == c)
        //    TRF = KeyCode.None;
        //else if (TL == c)
        //    TL = KeyCode.None;
        //else if (TRT == c)
        //    TRT = KeyCode.None;
    }
    private void SetRightTilt(KeyCode c)
    {
        TRT = c;
        //if (TLF == c)
        //    TLF = KeyCode.None;
        //else if (TRF == c)
        //    TRF = KeyCode.None;
        //else if (TL == c)
        //    TL = KeyCode.None;
        //else if (TLT == c)
        //    TLT = KeyCode.None;
    }
}

[Serializable]
public enum Keys
{
    None,
    LFlipper,
    RFlipper,
    Launch,
    LTilt,
    RTilt
}