using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DayNightTimer : MonoBehaviour
{
    private float m_time = 0f;
    private int m_minutes = 0;
    private int m_hours = 0;
    private int m_days = 0;

    private Text m_date_UI;
    private Text m_timeOfDay_UI;

    public int m_curDay = 1;
    public int m_curMonth = 1;
    public int m_curYear = 2090;
    public string m_dateString = "dddd, MMMM, yyyy";
    public string m_timeOfDayString = "hh:mm tt";
    void Start()
    {
        m_date_UI = GameObject.Find("DateLabel").GetComponent<Text>();
        m_timeOfDay_UI = GameObject.Find("TimeOfDayLabel").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        m_time += Time.deltaTime;
        m_minutes =  (int)m_time % 60;

        if(m_minutes > 60)
        {
            m_hours++;
            //Every hour villagers needs_levels will drop by their appropriate rates
        }

        if(m_hours >= 24)
        {
            m_hours = 0;
            IncrementDay();
        }

        if(m_days > 30)
        {
            m_curMonth++;
        }

        if(m_curMonth > 12)
        {
            m_curYear++;
        }

        UpdateUILabels();
    }

    private void UpdateUILabels()
    {
        DateTime curDate = new DateTime(m_curYear, m_curMonth, m_curDay);
        m_date_UI.text = curDate.ToString(m_dateString);

        DateTime curTime = new DateTime(1, 1, 1, m_hours, m_minutes, 1);
        m_timeOfDay_UI.text = curTime.ToString(m_timeOfDayString);
    }

    private void IncrementDay()
    {
        m_curDay++;
        m_days++;

        if (m_curDay > 7)
        {
            m_curDay = 1;
        }
    }
}
