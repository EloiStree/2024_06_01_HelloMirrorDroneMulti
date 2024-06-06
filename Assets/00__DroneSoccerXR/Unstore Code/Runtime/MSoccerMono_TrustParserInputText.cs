﻿using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_TrustParserInputText : MonoBehaviour
{

    public string m_textFormat= "DRONEALIAS:ROTATE HORIZONTAL:MOVE VERTICAL:MOVE HORIZONTAL:MOVE FRONTAL";

    public string m_lastTextReceived;
    public List<DroneToAffect> m_dronesToAffect;


    [System.Serializable]
    public class DroneToAffect
    {
        public MirrorMono_CallableUniqueAlias m_uniqueAlias;
        public MSoccerMono_AbstractGamepad m_gamepad;
    }


    private void Awake()
    {
        //set invariant culture
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
    }

    public void PushTextAsCommand(string textCommand)
    {

        m_lastTextReceived = textCommand;
        if (this.enabled == false)
            return;
        if (this.gameObject.activeInHierarchy == false)
            return;

       //DRONEALIAS:ROTATEHORIZONTAL:MOVE VERTICAL:MOVE HORIZONTAL:MOVE FRONTAL

        string[] tokens = textCommand.Split(new char[] { ':','|' } );
        if(tokens.Length != 5)
        {
            return;
        }


        float joystickLeftXPercent = 0;
        float joystickLeftYPercent = 0;
        float joystickRightXPercent = 0;
        float joystickRightYPercent = 0;

        string droneAlias = tokens[0];
        if(!float.TryParse(tokens[1], out joystickLeftXPercent))joystickLeftXPercent = 0;
        if (!float.TryParse(tokens[2], out joystickLeftYPercent))joystickLeftYPercent = 0; 
        if (!float.TryParse(tokens[3], out joystickRightXPercent))joystickRightXPercent = 0; 
        if (!float.TryParse(tokens[4], out joystickRightYPercent))joystickRightYPercent = 0; 

        foreach (var item in m_dronesToAffect)
        {
            if (item.m_uniqueAlias.IsAliasIn( droneAlias, true))
            {
                item.m_gamepad.SetHorizontalRotation(joystickLeftXPercent);
                item.m_gamepad.SetVerticalMove(joystickLeftYPercent);
                item.m_gamepad.SetHorizontaMove(joystickRightXPercent);
                item.m_gamepad.SetFrontalMove(joystickRightYPercent);
            }
        }
    }
}
