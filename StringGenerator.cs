using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestHime;
namespace _4Sem_1
{
    class MainClass
    {
        public static void Main()
        {
            //StringGenerator.Do();         
            Console.WriteLine("Enter amount of strings: ");
            string inp = Console.ReadLine();
            int count;
            if (int.TryParse(inp, out count) && count > 0)
            {
                Finder.Do(count);
            }
            else
            {
                Console.WriteLine("Incorrect amount");
                Console.ReadLine();
            }
        }
    }

    class StringGenerator
    {
        const string digits = "0123456789";
        const string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._";
        const string alphaDigits = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._0123456789";
        Random random = new Random();

        public static void Do()
        {
            Console.WriteLine("Enter amount of strings: ");
            string inp = Console.ReadLine();
            int count;
            if (int.TryParse(inp, out count) && count > 0)
            {
                StringGenerator generator = new StringGenerator();
                generator.GenerateFile(@"D:\LabFiles\Lab1_Strings_" + count + ".txt", count);
            }
            else
            {
                Console.WriteLine("Incorrect amount");
                Console.ReadLine();
            }
        }

        public List<string> Generate(int totalCount)
        {
            List<string> resultStrings = new List<string>();
            int correctCount = totalCount / 2;
            int incorrectCount = totalCount - correctCount;


            List<string> goodGoals = new List<string>();
            for (int i = 0; i < correctCount; i++)
            {
                goodGoals.Add(GenerateGoal());
            }
            for (int i = 0; i < correctCount; i++)
            {
                resultStrings.Add(CreateString(true, goodGoals));
            }


            List<string> badGoals = new List<string>();
            for (int i = 0; i < incorrectCount; i++)
            {
                badGoals.Add(GenerateGoal(false));
            }

            List<string> badAndGood = new List<string>();
            badAndGood.AddRange(badGoals);
            badAndGood.AddRange(goodGoals);

            for (int i = 0; i < incorrectCount; i++)
            {
                string s; ;
                int c = random.Next(0, 5);
                switch (c)
                {
                    case 0:
                        {
                            s = CreateString(false, badGoals);
                            break;
                        }
                    case 1:
                        {
                            s = CreateString(true, badGoals);
                            break;
                        }
                    case 2:
                        {
                            s = CreateString(false, badAndGood);
                            //s += " " + badGoals
                            break;
                        }
                    case 3:
                        {
                            s = CreateString(true, badAndGood);
                            break;
                        }
                    case 4:
                        {
                            int l = random.Next(0, 20);
                            StringBuilder builder = new StringBuilder(l);
                            for (int k = 0; k < l; k++)
                            {
                                builder.Append(Convert.ToChar(random.Next(char.MinValue, 255)));
                            }
                            s = builder.ToString();
                            break;
                        }
                    default:
                        {
                            s = "";
                            break;
                        }
                }
                resultStrings.Add(s);
            }
            return resultStrings;
        }

        string CreateString(bool isNeedToTryCreateCorrect, List<string> baseGoals)
        {
            List<string> str = new List<string>();
            str.Add(baseGoals[random.Next(0, baseGoals.Count)]);

            int length = 0;
            length += str[0].Length;

            int subGoalCount = random.Next(0, 7);
            if (subGoalCount >= 5 || subGoalCount >= baseGoals.Count)
            {
                subGoalCount = 0;
            }

            for (int j = 0; j < subGoalCount; j++)
            {
                int protect = 0;
                string temp = baseGoals[random.Next(0, baseGoals.Count)];
                if (isNeedToTryCreateCorrect && str.Contains(temp))
                {
                    j--;
                    protect++;
                    if (protect > 5)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    str.Add(temp);
                    length += temp.Length;
                }
            }
            StringBuilder builder = new StringBuilder(length + str.Count + 1);
            builder.Append(str[0]);
            builder.Append(":");
            for (int j = 1; j < str.Count; j++)
            {
                builder.Append(" ");
                builder.Append(str[j]);
            }
            return builder.ToString();
        }
        public void GenerateFile(string filePath, int count)
        {
            StreamWriter fs = new StreamWriter(new FileStream(filePath, FileMode.Create));
            if (fs != null)
            {
                List<string> strings = Generate(count);
                foreach (string x in strings)
                {
                    fs.WriteLine(x);
                }
                fs.WriteLine("_________");
                fs.WriteLine("String count: " + strings.Count);
                Console.WriteLine("All correct. Text file with " + count + " was created");
                fs.Close();
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();
        }
        string GenerateGoal(bool correct = true)
        {
            if (correct)
            {
                int l = random.Next(0, 10);
                StringBuilder builder = new StringBuilder(l);

                builder.Append(alpha[random.Next(alpha.Length)]);
                for (int i = 0; i < l; ++i)
                {
                    builder.Append(alphaDigits[random.Next(alpha.Length)]);
                }
                return builder.ToString();
            }
            else
            {
                int l = random.Next(0, 10);
                StringBuilder builder = new StringBuilder(l);
                builder.Append(digits[random.Next(digits.Length)]);
                for (int i = 0; i < l; ++i)
                {
                    builder.Append(Convert.ToChar(random.Next(char.MinValue, 255)));
                }
                return builder.ToString();
            }
        }
    }
    class Finder
    {
        Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
        static string filePath = @"D:\LabFiles\Lab1_Strings_";
        public static void Do(int stringCount)
        {
            long startTick = DateTime.Now.Ticks;
            Finder finder = new Finder();
            finder.Read(filePath.ToString() + stringCount.ToString() + ".txt");
            List<string> correctGoals = finder.FindCorrectConditionGoals();

            StreamWriter fs = new StreamWriter(new FileStream(filePath.ToString() + stringCount.ToString() + "_Result.txt", FileMode.Create));
            if (fs != null)
            {
                foreach (string x in correctGoals)
                {
                    fs.WriteLine(x);
                }
                fs.WriteLine("______");
                fs.WriteLine("Correct goals count: " + correctGoals.Count);
                fs.WriteLine("Ticks count for procces: " + (DateTime.Now.Ticks - startTick).ToString());
                Console.WriteLine("All correct. Text file with " + correctGoals.Count + " was created");
                Console.WriteLine("Ticks count for procces: " + (DateTime.Now.Ticks - startTick).ToString());
                fs.Close();
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();

            /*
            foreach (var x in finder.keyValuePairs)
            {
                Console.WriteLine("Goal: " + x.Key);
                Console.Write("\t Sub Goals: ");
                if (x.Value != null)
                {
                    foreach (string subGoal in x.Value)
                    {
                        Console.Write(subGoal + ", ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            foreach(string x in correctGoals)
            {
                Console.WriteLine(x);
            }
            
            Console.ReadLine();*/

        }
        void Read(string filePath)
        {
            StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open));
            if (sr != null)
            {
                string str;
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();
                    /*
                    if(IsCorrectString(str) != IsCorrectStringHime(str))//Debug
                    {
                        IsCorrectStringHime(str);
                        Console.WriteLine("Error: " + str);
                    }*/
                    
                    string[] captureListRE;
                    string[] captureListHime;

                    /*
                    if(IsCorrectStringCaptureList(str, out captureListRE) != IsCorrectStringSMCCaptureList(str, out captureListHime) )
                        //&& IsCorrectStringHimeCaptureList(str, out captureListRE) != IsCorrectString(str))
                    {
                        string[] deb;
                        IsCorrectStringCaptureList(str, out deb);
                        Console.WriteLine("Error: " + str); 
                    }*/
                    
                    string[] captureList;
                    if (IsCorrectStringSMCCaptureList(str, out captureList))//IsCorrectString(str))
                    {                     
                        /*
                        int ind = str.IndexOf(':');
                        string goal = str.Substring(0, ind);
                        string[] subGoals = str.Remove(0, ind + 1).Split(' ');
                        List<string> resSubGoals = new List<string>();
                        foreach (string s in subGoals)
                        {
                            s.Trim(' ');
                            if (!string.IsNullOrEmpty(s))
                            {
                                resSubGoals.Add(s);
                            }
                        }*/



                        /*IsCorrectStringCaptureList(str, out captureList);
                        for (int i = 0; i < captureListRE.Length; i++)
                        {
                            if (captureListRE[i] != captureListHime[i])
                            {
                                Console.WriteLine("Error: " + str);
                            }
                        }
                        //Debug

                        for (int i =0; i < resSubGoals.Count; i++)
                        {
                            if(resSubGoals[i] != captureList[i+1])
                            {
                                Console.WriteLine("Error: " + str);
                            }                            
                        }
                        if(goal != captureList[0])
                        {
                            Console.WriteLine("Error: " + str);
                        }*/

                        //
                        string goal = captureList[0];
                        List<string> resSubGoals = new List<string>();
                        for(int i = 1; i < captureList.Length; i++)
                        {
                            resSubGoals.Add(captureList[i]);
                        }
                        //

                        List<string> temp;
                        if (keyValuePairs.TryGetValue(goal, out temp))
                        {
                            if (resSubGoals.Count > 0)
                            {
                                if (temp != null)
                                {
                                    temp.AddRange(resSubGoals);
                                }
                                else
                                {
                                    keyValuePairs[goal] = resSubGoals;
                                }
                            }
                        }
                        else
                        {
                            if (resSubGoals.Count > 0)
                            {
                                keyValuePairs.Add(goal, resSubGoals);
                            }
                            else
                            {
                                keyValuePairs.Add(goal, null);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error on readfile");
            }
        }
        
        bool IsCorrectString(string str)
        {
            //return true;
            //string pattern = @"[a-zA-Z\._]w*";
            string pattern = @"^([a-zA-Z\._]([a-zA-Z\._0-9])*:)((\s)*[a-zA-Z\._][a-zA-Z\._0-9]*(\s)*)*$";
            if (Regex.IsMatch(str, pattern))
            {
                return true;
            }
            return false;
        }

        bool IsCorrectStringCaptureList(string str, out string[] captureList)
        {
            string pattern = @"(?n)(?x)^( (?'1'[a-zA-Z\._]([a-zA-Z\._0-9])*) :) ((\s)* (?'2'[a-zA-Z\._][a-zA-Z\._0-9]*) (\s)*)*$";
            Match match = Regex.Match(str, pattern);
            captureList = null;

            if (match.Success && match.Groups[1].Captures.Count == 1)
            {
                captureList = new string[1 + match.Groups[2].Captures.Count];
                captureList[0] = match.Groups[1].Value;
                for (int i = 0; i < match.Groups[2].Captures.Count; i++)
                {
                    captureList[i + 1] = match.Groups[2].Captures[i].Value;
                }
                return true;
            }
            return false;
        }
        bool IsCorrectStringSM(string str)
        {
            return StateMachine.IsStringCorrect(str);
        }
        bool IsCorrectStringSMC(string str)
        {
            return Detector.IsStringCorrect(str);
        }
        bool IsCorrectStringSMCCaptureList(string str, out string[] captureList)
        {
            return Detector.IsStringCorrectSMCCaptureList(str, out captureList);
        }
        bool IsCorrectStringHime(string str)
        {
            return Program.IsCorrectStringHime(str);
        }
        bool IsCorrectStringHimeCaptureList(string str, out string[] captureList)
        {
            return Program.IsCorrectStringHimeCaptureList(str, out captureList);
        }

        List<string> FindCorrectConditionGoals()
        {
            List<string> correctGoals = new List<string>();
            foreach (var x in keyValuePairs)
            {
                if (x.Value != null)
                {
                    foreach (string g in x.Value)
                    {
                        if (correctGoals.Contains(g))
                        {
                            continue;
                        }
                        if (!keyValuePairs.ContainsKey(g) || keyValuePairs[g] == null)
                        {
                            correctGoals.Add(g);
                        }
                    }
                }
            }
            return correctGoals;
        }
    }



    public enum MachineStatus { errorState, work, receivingState};
    public abstract class State
    {
        protected StateMachine stateMachine;
        public State(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }
        public virtual void DefaultInput()
        {
            stateMachine.SetState(new ErrorState(stateMachine));
        }
        public virtual void AlfaInput()
        {
            DefaultInput();
        }
        public virtual void DigitInput()
        {
            DefaultInput();
        }
        public virtual void SeparatorCharInput()
        {
            DefaultInput();
        }
        public virtual void SpaceInput()
        {
            DefaultInput();
        }
        public virtual void EndStringInput()
        {
            DefaultInput();
        }
        public virtual MachineStatus GetCurrentMachineStatus()
        {
            return MachineStatus.work;
        }
    }
    public class StateMachine
    {
        static readonly string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._";
        static readonly string digits = "0123456789";
        static readonly char separatorChar = ':';
        State currentState;

        public static bool IsStringCorrect(string str)
        {
            StateMachine stateMachine = new StateMachine();
            for (int i = 0; i < str.Length; i++)
            {
                stateMachine.SetCharInput(str[i]);
                if(stateMachine.IsErrorStatus())
                {
                    return false;
                }
            }
            stateMachine.SetCharInput('\0');
            return stateMachine.IsRecievingStatus();
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
        bool IsWorkStatus()
        {
            if(currentState.GetCurrentMachineStatus() == MachineStatus.work)
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
            if (currentState.GetCurrentMachineStatus() == MachineStatus.receivingState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IsErrorStatus()
        {
            if(currentState.GetCurrentMachineStatus() == MachineStatus.errorState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public StateMachine()
        {
            SetState(new AState(this));
        }
        public void SetState(State state)
        {
            currentState = state;
        }
    }
    public class ErrorState : State
    {
        public ErrorState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void DefaultInput()
        {

        }
        public override MachineStatus GetCurrentMachineStatus()
        {
            return MachineStatus.errorState;
        }
    }
    public class AState : State
    {
        public AState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void AlfaInput()
        {
            stateMachine.SetState(new BState(stateMachine));
        }
    }
    public class BState : State
    {
        public BState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void AlfaInput()
        {

        }
        public override void DigitInput()
        {
           
        }
        public override void SeparatorCharInput()
        {
            stateMachine.SetState(new CState(stateMachine));
        }
    }
    public class CState : State
    {
        public CState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void SpaceInput()
        {

        }
        public override void AlfaInput()
        {
            stateMachine.SetState(new DState(stateMachine));
        }
        public override void EndStringInput()
        {
            stateMachine.SetState(new FState(stateMachine));
        }
    }
    public class DState : State
    {
        public DState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void AlfaInput()
        {

        }
        public override void DigitInput()
        {

        }
        public override void SpaceInput()
        {
            stateMachine.SetState(new EState(stateMachine));
        }
        public override void EndStringInput()
        {
            stateMachine.SetState(new FState(stateMachine));
        }
    }
    public class EState : State
    {
        public EState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void SpaceInput()
        {

        }
        public override void EndStringInput()
        {
            stateMachine.SetState(new FState(stateMachine));
        }
        public override void AlfaInput()
        {
            stateMachine.SetState(new DState(stateMachine));
        }
    }
    public class FState : State
    {
        public FState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }
        public override void DefaultInput()
        {

        }
        public override MachineStatus GetCurrentMachineStatus()
        {
            return MachineStatus.receivingState;
        }
    }
}