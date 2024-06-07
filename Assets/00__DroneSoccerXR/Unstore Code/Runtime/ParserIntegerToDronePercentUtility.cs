using System;

public class ParserIntegerToDronePercentUtility
{

    public static void PushIntegerValueFromByte(byte[] byteCommand, out int targetDrone, out float joystickRightYPercent, out float joystickRightXPercent, out float joystickLeftYPercent, out float joystickLeftXPercent)
    {
        PushIntegerValue(BitConverter.ToInt32(byteCommand, 0), out targetDrone, out joystickRightYPercent, out joystickRightXPercent, out joystickLeftYPercent, out joystickLeftXPercent);
    }

    public static void PushIntegerValue(int integerCommand,
        out int targetDrone20to20,
        out float m_joystickLeftXPercent,
        out float m_joystickLeftYPercent,
        out float m_joystickRightXPercent,
        out float m_joystickRightYPercent
        )
    {
  
        //21 47 48 36 47
        targetDrone20to20 = (int)(integerCommand / 100000000);
        int joystickRightY = Math.Abs(integerCommand / 1 % 100);
        int joystickRightX = Math.Abs(integerCommand / 100 % 100);
        int joystickLeftY = Math.Abs(integerCommand / 10000 % 100);
        int joystickLeftX = Math.Abs(integerCommand / 1000000 % 100);


        Parser99ToPercent(joystickRightY,   out m_joystickRightYPercent );
        Parser99ToPercent(joystickRightX,   out m_joystickRightXPercent );
        Parser99ToPercent(joystickLeftY,    out m_joystickLeftYPercent  ) ;
        Parser99ToPercent(joystickLeftX,    out m_joystickLeftXPercent  );

    }

  

    private static void Parser99ToPercent(float value99, out float percent11)
    {
        if (value99 == 0)
        {
            percent11 = 0;
            return;
        }

        percent11 = (((value99 - 1) / 98) - 0.5f) * 2f;
    }
}