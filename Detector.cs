using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Detector
{
    static readonly string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._";
    static readonly string digits = "0123456789";
    static readonly char separatorChar = ':';
    DetectorContext currentState;

    int curIndex = 0;
    public List<Pos> strPos = new List<Pos>();
    Detector()
    {
        currentState = new DetectorContext(this);
        currentState.EnterStartState();
        strPos.Add(new Pos(0, -1));
    }

    public void IncCurrentIndex()
    {
        curIndex++;
    }
    public void SaveStartIndex()
    {
        strPos.Add(new Pos(curIndex, -1));
    }
    public void SaveEndIndex()
    {
        strPos[strPos.Count - 1].end = curIndex;
    }
    public static bool IsStringCorrect(string str)
    {
        Detector stateMachine = new Detector();
        for (int i = 0; i < str.Length; i++)
        {
            stateMachine.SetCharInput(str[i]);
            if (stateMachine.IsErrorStatus())
            {
                return false;
            }
        }
        stateMachine.SetCharInput('\0');
        return stateMachine.IsRecievingStatus();
    }
    public static bool IsStringCorrectSMCCaptureList(string str, out string[] captureList)
    {
        Detector stateMachine = new Detector();
        captureList = null;
        for (int i = 0; i < str.Length; i++)
        {
            stateMachine.SetCharInput(str[i]);
            if (stateMachine.IsErrorStatus())
            {
                
                return false;
            }
        }
        stateMachine.SetCharInput('\0');

        if(stateMachine.IsRecievingStatus())
        {
            List<string> temp = new List<string>();
            for(int i = 0; i < stateMachine.strPos.Count; i++)
            {
                temp.Add(str.Substring(stateMachine.strPos[i].start, stateMachine.strPos[i].end - stateMachine.strPos[i].start));
            }
            captureList = temp.ToArray();
            return true;
        }
        else
        {
            return false;
        }
    }
    void SetCharInput(char ch)
    {
        if (ch == '\0')
        {
            currentState.EndStringInput();
        }
        else if (ch == ' ')
        {
            currentState.SpaceInput();
        }
        else if (alpha.Contains(ch))
        {
            currentState.AlfaInput();
        }
        else if (digits.Contains(ch))
        {
            currentState.DigitInput();
        }
        else if (ch == separatorChar)
        {
            currentState.SeparatorCharInput();
        }
        else
        {
            currentState.DefaultInput();
        }
    }
    bool IsErrorStatus()
    {
        if (currentState.State == DetectorContext.MainMap.Error)// currentState.GetCurrentMachineStatus() == MachineStatus.errorState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool IsRecievingStatus()
    {
        if (currentState.State == DetectorContext.MainMap.Recieve)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public class Pos
    {
        public Pos(int s, int e)
        {
            start = s;
            end = e;
        }
        public int start = -1;
        public int end = -1;
    }

}

