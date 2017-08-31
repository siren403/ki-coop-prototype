package com.kids.charlie;

import android.content.Context;
import android.os.Bundle;
import android.telephony.TelephonyManager;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //UnityPlayer.UnitySendMessage("InstSceneFirebase","CallFromNative","Native Call Test");
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
