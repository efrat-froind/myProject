
namespace DO;

[Serializable]
public class dalTheEntityDoesNotExist:Exception
{
    public dalTheEntityDoesNotExist(string e)
    {
        throw new Exception(e);
    }
}

[Serializable]
public class dalTheCodeAlreadyExists: Exception
{
    public dalTheCodeAlreadyExists(string e)
    {
        throw new Exception(e);
    }
}

