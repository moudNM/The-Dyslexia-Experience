using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class LetterScrambler : MonoBehaviour
{
    static System.Random rnd = new System.Random();

    //flips letters from p to q
    public static List<string> fromPtoQ(List<string> words)
    {

        int noOfLetters = 0;
        foreach (string word in words)
        {
            if (word.Contains("p") || word.Contains("q"))
            {
                noOfLetters++;
            }

        }
        //int amtChanged = 0;
        int amtOfLettersLeft = noOfLetters;
        int amtToChange = rnd.Next(0, noOfLetters+1);
        bool[] coin = { true, false };

        for (int i = 0; i < words.Count; i++)
        {

            if (words[i].Contains("p") || words[i].Contains("q"))
            {
                bool b = coin[rnd.Next(0, coin.Length)];
                if(amtToChange == amtOfLettersLeft)b = true;

                if (b)
                {
                    amtToChange--;
                    //do the sub
                    if (words[i].Contains("p")){
                            StringBuilder sb = new StringBuilder(words[i]);
                            sb.Replace('p', 'q');
                            words[i] = sb.ToString();  
                    }
                    else
                    {
                        if (words[i].Contains("q"))
                        {
                            StringBuilder sb = new StringBuilder(words[i]);
                            sb.Replace('q', 'p');
                            words[i] = sb.ToString();
                        }
                    }

                }
                amtOfLettersLeft--;
            }

            if (amtToChange == 0)
            {
                break;
            }

        }
        return words;
    }

    //flips letters from b to d
    public static List<string> fromBtoD(List<string> words)
    {

        int noOfLetters = 0;
        foreach (string word in words)
        {
            if (word.Contains("b") || word.Contains("d"))
            {
                noOfLetters++;
            }

        }
        //int amtChanged = 0;
        int amtOfLettersLeft = noOfLetters;
        int amtToChange = rnd.Next(0, noOfLetters+1);
        bool[] coin = { true, false };


        for (int i = 0; i < words.Count; i++)
        {

            if (words[i].Contains("b") || words[i].Contains("d"))
            {
                bool b = coin[rnd.Next(0, coin.Length)];
                if (amtToChange == amtOfLettersLeft) b = true;

                if (b)
                {
                    amtToChange--;
                    //do the sub
                    if (words[i].Contains("b"))
                    {
                        StringBuilder sb = new StringBuilder(words[i]);
                        sb.Replace('b', 'd');
                        words[i] = sb.ToString();
                    }
                    else
                    {
                        if (words[i].Contains("d"))
                        {
                            StringBuilder sb = new StringBuilder(words[i]);
                            sb.Replace('d', 'b');
                            words[i] = sb.ToString();
                        }
                    }

                }
                amtOfLettersLeft--;
            }

            if (amtToChange == 0)
            {
                break;
            }

        }
        return words;
    }

}
        


  

