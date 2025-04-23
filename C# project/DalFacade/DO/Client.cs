

namespace DO;

/// <summary>
/// רשומה לפרטי לקוח
/// </summary>
/// <param name="_id">תעודת זהות</param>
/// <param name="_userName">שם הלקוח</param>
/// <param name="_address">כתובת לקוח</param>
/// <param name="_phone">טלפון לקוח</param>
public record Client
    (int _id,string _userName, string? _address,int? _phone)
{
    public Client():this(0,null,null,0)
    {
        
    }
}
