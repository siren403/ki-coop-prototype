package com.kids.charlie;

import android.app.Activity;
import android.content.Intent;

/**
 * Created by SEONG on 2017-09-04.
 */

public class ActivityLauncher {

    public static void launchActivity(String type, final Activity m_activity){

        Intent i = new Intent();
        i.setAction(Intent.ACTION_MAIN);
        i.setClassName(m_activity, type);
        m_activity.startActivity(i);
    }

}
