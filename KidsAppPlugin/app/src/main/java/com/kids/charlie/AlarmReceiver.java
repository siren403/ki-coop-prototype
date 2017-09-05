package com.kids.charlie;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.util.Log;
import android.widget.Toast;

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
                context.startActivity(i);
            }
        }
        catch (Exception e){
            Toast.makeText(context,"Not installed charlie app",Toast.LENGTH_LONG);
        }
//        Intent serviceIntent = new Intent(context,AlarmToastService.class);
//        context.startService(serviceIntent);
    }
}
