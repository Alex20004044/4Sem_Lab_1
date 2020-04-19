using System;
using System.Collections.Generic;
using Hime.Redist; // the namespace for the Hime Runtime
using MathExp; // default namespace for the parser is the grammar's name
using Hime.Redist.Utils;
using Hime.Redist.Lexer;
namespace TestHime
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            IsCorrectStringHime("tJFCXIF: a[");
            IsCorrectStringHime("Ylslw: tJFCXIF 1nOyR.QavO");
            IsCorrectStringHime("Ylslw: t1JFCXIF 1nOyR.QavO");
        }
        public static bool IsCorrectStringHime(string str)
        {
            MathExpLexer lexer = new MathExpLexer(str);
            bool isContainsGoal = false;
            while (true)
            {
                Token t = lexer.GetNextToken();
                //string ss = t.Value;
                //string s = t.Context.Content;
                if (t.Symbol.Name == "TRASH")
                {
                    return false;
                }
                else if (t.Symbol.Name == "GOAL")
                {
                    if (isContainsGoal)
                    {
                        return false;//Two Goals in one string
                    }
                    isContainsGoal = true;
                }
                if (t.Symbol.ID == Symbol.SID_DOLLAR)
                {
                    break;
                }
            }
            if (isContainsGoal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsCorrectStringHimeCaptureList(string str, out string[] captureList)
        {
            MathExpLexer lexer = new MathExpLexer(str);
            bool isContainsGoal = false;
            captureList = null;
            List<string> temp = new List<string>();


            while (true)
            {
                Token t = lexer.GetNextToken();
                //string ss = t.Value;
                //string s = t.Context.Content;
                if (t.Symbol.Name == "TRASH")
                {
                    return false;
                }
                else if (t.Symbol.Name == "GOAL")
                {
                    if (isContainsGoal)
                    {
                        return false;//Two Goals in one string
                    }
                    else
                    {
                        temp.Add(t.Value.TrimEnd(':'));
                        isContainsGoal = true;
                    }
                }
                else if(t.Symbol.Name == "SUBGOAL")
                {
                    if(temp.Count == 0)
                    {
                        return false; //First token is not Goal
                    }
                    temp.Add(t.Value);
                }
                if (t.Symbol.ID == Symbol.SID_DOLLAR)
                {
                    break;
                }
            }
            if (isContainsGoal)
            {
                captureList = temp.ToArray();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}