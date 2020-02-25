using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    DateTime currentDate;
    DateTime oldDate;
    void Start()
    {
        if (!PlayerPrefs.HasKey("oldDate"))
        {
            return;
        }
        //Store the current time when it starts
        currentDate = GetNetTime();

        //Grab the old time from the player prefs as a long
        long temp = Convert.ToInt64(PlayerPrefs.GetString("oldDate"));

        //Convert the old time from binary to a DataTime variable
        DateTime oldDate = DateTime.FromBinary(temp);
        print("oldDate: " + oldDate);

        //Use the Subtract method and store the result as a timespan variable
        TimeSpan difference = currentDate.Subtract(oldDate);
        print("Difference: " + difference);

    }

    void OnApplicationQuit()
    {
        //Savee the current system time as a string in the player prefs class
        //PlayerPrefs.SetString("oldDate", GetNetTime().ToBinary().ToString());

        //print("Saving this date to prefs: " + GetNetTime());
    }

    public DateTime GetNetTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
        "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
        CultureInfo.InvariantCulture.DateTimeFormat,
        DateTimeStyles.AssumeUniversal);
    }

}
