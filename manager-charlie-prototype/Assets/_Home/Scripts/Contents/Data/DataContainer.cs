using System;

namespace Contents.Data
{
    [System.Obsolete("Not Used Data format")]
    public abstract class DataContainer
    {
        [System.Obsolete("Not Used Data format")]
        public virtual LitJson.JsonData GetData(int contentsID) { throw new NotImplementedException(); }
        [System.Obsolete("Not Used Data format")]
        public virtual QuickSheet.Contents1 GetQnAData(int episodeID) { throw new NotImplementedException(); }
    }
}
