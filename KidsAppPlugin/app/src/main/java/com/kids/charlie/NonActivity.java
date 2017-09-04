package com.kids.charlie;

/**
 * Created by SEONG on 2017-09-04.
 */

public class NonActivity {

    public static String GetStaticString(String a)
    {
        return "Plugin static: " + a;
    }

    public static int GetStaticInt(int a)
    {
        return a + 1000;
    }

    public String GetString(String a)
    {
        return "Plugin object: " + a;
    }

    public int GetInt(int a)
    {
        return a + 50000;
    }

}
