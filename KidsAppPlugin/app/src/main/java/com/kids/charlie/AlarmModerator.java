package com.kids.charlie;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.icu.util.Calendar;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

/**
 * Created by SEONG on 2017-09-04.
 */

public class AlarmModerator {

    private static final String INTENT_ALARM_START = "com.kids.charlie.ALRAM_START";
    public static final String INTENT_UNITY_ALARM_ID = "UnityAlarmID";

    public static void launchActivity(String type, final Activity m_activity){

        Intent i = new Intent();
        i.setAction(Intent.ACTION_MAIN);
        i.setClassName(m_activity, type);
        m_activity.startActivity(i);
    }

    public static void setAlram(int id,int hour,int min,int sec, String callback){

        Log.i("Unity","id:"+id+"/hour:"+hour+"/min:"+min+"/sec:"+sec+"/callbackName:"+callback);

        Calendar calender = Calendar.getInstance();
        calender.set(Calendar.HOUR_OF_DAY, hour);
        calender.set(Calendar.MINUTE, min);
        calender.set(Calendar.SECOND, sec);

        Intent alarmIntent = new Intent(INTENT_ALARM_START);
        alarmIntent.putExtra(INTENT_UNITY_ALARM_ID,id);
        Log.i("Unity", String.valueOf(alarmIntent.getIntExtra(INTENT_UNITY_ALARM_ID, 0)));
        PendingIntent pendingIntent = PendingIntent.getBroadcast(
                UnityPlayer.currentActivity,
                id,
                alarmIntent,
                PendingIntent.FLAG_UPDATE_CURRENT
        );

        AlarmManager alarmManager = (AlarmManager)UnityPlayer.currentActivity.getSystemService(Context.ALARM_SERVICE);
        alarmManager.setAndAllowWhileIdle(
                AlarmManager.RTC_WAKEUP,
                calender.getTimeInMillis(),
                pendingIntent
        );


        UnityPlayer.UnitySendMessage(
                MainActivity.UNITY_ANDROID_MANAGER_NAME,
                callback,
                "Success Set Alarm : " + id
        );
    }
}
