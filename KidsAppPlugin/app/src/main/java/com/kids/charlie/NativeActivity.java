package com.kids.charlie;

import android.app.Activity;
import android.os.Bundle;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

/**
 * Created by SEONG on 2017-09-04.
 */

public class NativeActivity extends Activity {

    @Override
    protected void onCreate(Bundle bundle){
        super.onCreate(bundle);

        showToast();
    }

    public void showToast(){
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(activity,"successful!!",Toast.LENGTH_LONG).show();
            }
        });
    }
}
