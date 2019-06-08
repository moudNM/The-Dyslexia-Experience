using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class SentenceLetterScrambler : MonoBehaviour
{
    static System.Random rnd = new System.Random();

    public static string fromBtoD(string sentence)
    {
        int noOfLetters = 0;

        foreach (char c in sentence)
        {
            if (c == 'b' || c == 'd')
            {
                noOfLetters++;
            }

        }

        StringBuilder sb = new StringBuilder(sentence);

        int amtOfLettersLeft = noOfLetters;             //no of b and d left
        int amtToChange = rnd.Next(0, noOfLetters + 1); //no of b and d to change
        bool[] coin = { true, false };                  //to change or not to change

        for (int i = 0; i < sb.Length; i++)
        {

            if (sb[i] == 'b' || sb[i] == 'd')
            {

                bool b = coin[rnd.Next(0, coin.Length)];

                if (amtOfLettersLeft <= amtToChange)
                {
                    //must change
                    b = true;
                }

                if (b)
                {
                    if (sb[i] == 'b')
                    {
                        sb[i] = 'd';
                    }
                    else if (sb[i] == 'd')
                    {
                        sb[i] = 'b';
                    }
                    amtToChange--;
                    amtOfLettersLeft--;
                }
            }

            if (amtToChange == 0)
            {
                break;
            }
        }

        sentence = sb.ToString();

        return sentence;
    }


    public static string fromPtoQ(string sentence)
    {
        int noOfLetters = 0;

        foreach (char c in sentence)
        {
            if (c == 'p' || c == 'q')
            {
                noOfLetters++;
            }

        }

        StringBuilder sb = new StringBuilder(sentence);

        int amtOfLettersLeft = noOfLetters;             //no of p and q left
        int amtToChange = rnd.Next(0, noOfLetters + 1); //no of p and q to change
        bool[] coin = { true, false };                  //to change or not to change

        for (int i = 0; i < sb.Length; i++)
        {

            if (sb[i] == 'p' || sb[i] == 'q')
            {

                bool b = coin[rnd.Next(0, coin.Length)];

                if (amtOfLettersLeft <= amtToChange)
                {
                    //must change
                    b = true;
                }

                if (b)
                {
                    if (sb[i] == 'p')
                    {
                        sb[i] = 'q';
                    }
                    else if (sb[i] == 'q')
                    {
                        sb[i] = 'p';
                    }
                    amtToChange--;
                    amtOfLettersLeft--;
                }
            }

            if (amtToChange == 0)
            {
                break;
            }
        }

        sentence = sb.ToString();

        return sentence;
    }

    public static string scrambleWord(string newWord){
        string word = "";
        List<char> charList = new List<char>();
        bool notComplete = true;
        //convert word into char list.

        if(notComplete){
            word = "";
            charList = new List<char>(newWord.ToCharArray());

            while (charList.Count > 0)
            {
                //randomly add characters to word   
                int pos = rnd.Next(0, charList.Count);
                word += charList[pos];
                charList.RemoveAt(pos);
            }

            if(word != newWord){
                notComplete = false;
            }
        }

        return word;
    }
}



