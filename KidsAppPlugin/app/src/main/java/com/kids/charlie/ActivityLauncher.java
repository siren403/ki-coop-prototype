package com.kids.charlie;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.icu.util.Calendar;
import android.util.Log;

/**
 * Created by SEONG on 2017-09-04.
 */

public class ActivityLauncher {

    public static String AlarmIntentName = "com.kids.charlie.ALRAM_START";

    public static void launchActivity(String type, final Activity m_activity){

        Intent i = new Intent();
        i.setAction(Intent.ACTION_MAIN);
        i.setClassName(m_activity, type);
        m_activity.startActivity(i);
    }

    public static void setAlram(int requestCode,int hour,int min,int sec, final Activity m_activity){

        Log.i("Unity","req:"+requestCode+"/hour:"+hour+"/min:"+min+"/sec:"+sec);
        Calendar calender = Calendar.getInstance();
        calender.set(Calendar.HOUR_OF_DAY,hour);
        calender.set(Calendar.MINUTE,min);
        calender.set(Calendar.SECOND,sec);

        Intent alarmIntent = new Intent(AlarmIntentName);
        PendingIntent pendingIntent = PendingIntent.getBroadcast(
                m_activity,
                requestCode,
                alarmIntent,
                PendingIntent.FLAG_UPDATE_CURRENT
        );
        AlarmManager alarmManager = (AlarmManager)m_activity.getSystemService(Context.ALARM_SERVICE);
        alarmManager.set(
                AlarmManager.RTC_WAKEUP,
                calender.getTimeInMillis(),
                pendingIntent
        );
    }
}
