package com.kids.charlie;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.os.PowerManager;
import android.preference.PreferenceManager;
import android.util.Log;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

/**
 * Created by SEONG on 2017-09-05.
 */

public class AlarmReceiver extends BroadcastReceiver {

    @Override
    public void onReceive(Context context, Intent intent) {
        String packageName = "com.kids.charlie";



        try {
            PackageInfo info = context.getPackageManager().getPackageInfo(packageName, 0);
            if(info != null){
                Intent i = context.getPackageManager().getLaunchIntentForPackage(packageName);
                i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);

                SharedPreferences sharedPreferences = context.getSharedPreferences(MainActivity.ALARM_PREFS_NAME,0);
                SharedPreferences.Editor editor = sharedPreferences.edit();
                editor.putBoolean("hasAlarm", true);
                editor.putInt("alarmID",intent.getIntExtra(AlarmModerator.INTENT_UNITY_ALARM_ID,0));
                editor.commit();

                PowerManager pm = (PowerManager) context.getSystemService(Context.POWER_SERVICE);
                PowerManager.WakeLock sCpuWakeLock = pm.newWakeLock(
                        PowerManager.SCREEN_BRIGHT_WAKE_LOCK |
                                PowerManager.ACQUIRE_CAUSES_WAKEUP |
                                PowerManager.ON_AFTER_RELEASE, "FAIL");
                sCpuWakeLock.acquire();

                context.startActivity(i);

            }
        }
        catch (Exception e){
            Toast.makeText(context,"Not installed charlie app",Toast.LENGTH_LONG);
        }

//      Intent serviceIntent = new Intent(context,AlarmToastService.class);
//      context.startService(serviceIntent);
    }
}
