[System.Serializable]
public class InFix
{
    public string prefix;
    public string postFix;

    public string Get(string str)
    {
        return prefix + str + postFix;
    }
}