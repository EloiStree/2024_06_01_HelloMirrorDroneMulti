using System;

public class ParserIntegerToDronePercentUtility
{

    public static void Unpack(byte[] byteCommand, out int targetDrone, 
        out float joystickLeftXPercent,
        out float joystickLeftYPercent,
        out float joystickRightXPercent,
        out float joystickRightYPercent)
    {
        Unpack(BitConverter.ToInt32(byteCommand, 0), out targetDrone, out joystickRightYPercent, out joystickRightXPercent, out joystickLeftYPercent, out joystickLeftXPercent);
    }

    public static void Unpack(int integerCommand,
        out int targetDrone20to20,
        out float joystickLeftXPercent,
        out float joystickLeftYPercent,
        out float joystickRightXPercent,
        out float joystickRightYPercent
        )
    {

        //21 47 48 36 47
        targetDrone20to20 = (int)(integerCommand / 100000000);
        int joystickRightY = Math.Abs(integerCommand / 1 % 100);
        int joystickRightX = Math.Abs(integerCommand / 100 % 100);
        int joystickLeftY = Math.Abs(integerCommand / 10000 % 100);
        int joystickLeftX = Math.Abs(integerCommand / 1000000 % 100);


        Parser99ToPercent(joystickRightY, out joystickRightYPercent);
        Parser99ToPercent(joystickRightX, out joystickRightXPercent);
        Parser99ToPercent(joystickLeftY, out joystickLeftYPercent);
        Parser99ToPercent(joystickLeftX, out joystickLeftXPercent);

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

    public static void Pack(out int cmd, int fixedDroneId1To12, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        float signe = Math.Sign(fixedDroneId1To12);
        cmd = Math.Abs(fixedDroneId1To12) * 100000000;
        cmd += (int)ParserPercent11To99(backForward);
        cmd += (int)ParserPercent11To99(leftRight) * 100;
        cmd += (int)ParserPercent11To99(downUp) * 10000;
        cmd += (int)ParserPercent11To99(rotateHorizontal) * 1000000;
        if(signe<0f)
            cmd = -cmd;
    }

    public static void ParserPercent11To99(float percent11, out float value99)
    {
        if (percent11 == 0)
        {
            value99 = 0;
            return ;
        }

        value99 = (float)Math.Round((((percent11 + 1.0f) / 2.0f) * 98.0f) + 1.0f);
    }
    public static float ParserPercent11To99(float percent11)
    {
        if(percent11== 0)
            {
            return 0;
        }

        return Math.Clamp( (float)Math.Round((((percent11 + 1.0f) / 2.0f) * 98.0f) + 1.0f),0,99);
    }
}