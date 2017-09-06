package com.kids.charlie;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.telephony.TelephonyManager;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    public static final String UNITY_ANDROID_MANAGER_NAME  = "AndroidManager";
    public static final String ALARM_PREFS_NAME = "alarmData";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //UnityPlayer.UnitySendMessage("InstSceneFirebase","CallFromNative","Native Call Test");

        SharedPreferences sharedPreferences = this.getSharedPreferences(ALARM_PREFS_NAME,0);
        if(sharedPreferences.getBoolean("hasAlarm",false)) {
            UnityPlayer.UnitySendMessage(
                    MainActivity.UNITY_ANDROID_MANAGER_NAME,
                    "ReceiveString",
                    "WakeUp "+sharedPreferences.getInt("alarmID",0)
            );
        }else{
            Log.i("Unity","Not has alarm data");
        }
    }

    public String CallByUnityString(String str) {
        return "Plugin Object : "+str;
    }

    public String GetPhoneNumber(boolean isKrNumber) {
        String myNumber = null;
        TelephonyManager mgr = (TelephonyManager) getSystemService(Context.TELEPHONY_SERVICE);
        try {
            myNumber = mgr.getLine1Number();
            if(isKrNumber){
                myNumber = myNumber.replace("+82", "0");
            }
            return myNumber;
        } catch (Exception e) {
            return "PhoneNumber is Null";
        }
    }
}
