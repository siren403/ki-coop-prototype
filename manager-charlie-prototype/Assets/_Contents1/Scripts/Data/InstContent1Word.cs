using System.Collections;
using System.Collections.Generic;

public class InstContent1Word : InstContent1Alphabet
{
    /** 알파벳 관련 데이터를 저장하는 클래스 */
    /** */
    /** Word ID */
    public int WordIndex;

    /** TEXT */
    public string Word;

    #region 생성자들
    /** 생성자 */
    public InstContent1Word(int idx, char ch, int index, string word)
    {
        // super Class 
        AlphabetIdx = idx;
        Alphabet = ch;

        WordIndex = index;
        Word = word;
    }

    public InstContent1Word(int index, string word)
    {
        WordIndex = index;
        Word = word;
    }

    public InstContent1Word()
    {

    }
    #endregion
}