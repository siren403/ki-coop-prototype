using System.Collections;
using System.Collections.Generic;

public class InstContent1Alphabet
{
    /** 알파벳 클래스 멤버 */
    public int AlphabetIdx;        // 알파벳 idx
    public char Alphabet;          // 알파벳 문자

    #region 생성자들
    /** 생성자 */
    public InstContent1Alphabet()
    {

    }

    public InstContent1Alphabet(int idx, char ch)
    {
        AlphabetIdx = idx;
        Alphabet = ch;
    }
    #endregion

    //public int ALPHABETIDX
    //{
    //    get { return AlphabetIdx; }
    //}

    //public char ALPHABET
    //{
    //    get { return Alphabet; }
    //}
}
