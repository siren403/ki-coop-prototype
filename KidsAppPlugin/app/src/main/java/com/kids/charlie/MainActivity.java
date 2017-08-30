package com.kids.charlie;

import android.os.Bundle;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        UnityPlayer.UnitySendMessage("InstSceneFirebase","CallFromNative","Native Call Test");
    }

    public String CallByUnityString(String str) {
        return "Plugin Object : "+str;
    }
}
