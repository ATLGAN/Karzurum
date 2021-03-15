[System.Serializable]
public struct MinMaxFloat
{
    public float MinValue;
    public float MaxValue;

    public float Avarage 
    { 
        get 
        {
            return ((MaxValue + MinValue) / 2);
        } 
    }
    public float Sum
    {
        get
        {
            return MaxValue + MinValue;
        }
    }
    public float Sub
    {
        get
        {
            return MaxValue - MinValue;
        }
    }
}