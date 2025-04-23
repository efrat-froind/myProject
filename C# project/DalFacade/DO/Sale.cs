

namespace DO;

/// <summary>
/// רשומה לפרטי מבצע
/// </summary>
/// <param name="_id">מספר מזהה ייחודי</param>
/// <param name="_code">מספר מזהה של המוצר</param>
/// <param name="_amountForSale">כמות נדרשת לקבלת המבצע</param>
/// <param name="_priceForSale">מחיר כולל במבצע</param>
/// <param name="_isForEveryone">האם המבצע מיועד לכל הלוקוחות או רק ללקוחות מועדון</param>
/// <param name="_dateStart">תאריך תחילת המבצע</param>
/// <param name="_dateFinifh">תאריך סיום המבצע</param>
public record Sale
    (int _id,int _code,int _amountForSale,double? _priceForSale,Boolean? _isForEveryone,DateTime _dateStart,DateTime _dateFinifh)
{
    public Sale():this(0,0,0,0,false,DateTime.Now, DateTime.Now)
    {
        
    }
}
