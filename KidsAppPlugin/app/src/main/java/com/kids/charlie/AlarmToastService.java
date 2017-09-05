package com.kids.charlie;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.util.Log;
import android.widget.Toast;

/**
 * Created by SEONG on 2017-09-05.
 */

public class AlarmToastService extends Service {

    public AlarmToastService(){

    }

    @Override
    public int onStartCommand(Intent intent,int flags,int startId){
        Toast.makeText(this,"알람이 울립니다." + flags,Toast.LENGTH_LONG).show();
        return START_NOT_STICKY;
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}
