

namespace DO;

/// <summary>
/// רשומה לפרטי מוצר
/// </summary>
/// <param name="_code">קוד מוצר</param>
/// <param name="_name">שם מוצר</param>
/// <param name="_category">קטגוריה</param>
/// <param name="_price">מחיר מוצר</param>
/// <param name="_QuantityInStock">כמות במלאי</param>
/// <param name="_image">תמונת מוצר</param>
public record Product
    (int _code,string _name, Category? _category,double _price,int?  _QuantityInStock,string? _image)
{
    public Product():this(0,null,null,0,0,null)
    {

    }
}
