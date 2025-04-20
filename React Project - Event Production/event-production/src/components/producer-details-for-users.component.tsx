import { useParams } from "react-router-dom"; // מייבא את useParams מ-React Router.
import { getProducer } from "../api/producer.api"; // מייבא פונקציה ל-API.
import { useEffect, useState } from "react"; // מייבא useEffect ו-useState מ-React.

export const ProducerDetailsForUsers = () => {
    const { pMail } = useParams(); // מקבל את המייל מה-URL.
    const [producer, setProducer] = useState<any>(''); // מצב עבור פרטי המפיקה.

    // שימוש ב-useEffect כדי להביא את פרטי המפיקה מה-API כאשר הרכיב נטען.
    useEffect(() => {
        getProducer(pMail) // קריאה ל-API עם המייל.
            .then(fetchedProducer => {
                setProducer(fetchedProducer); // עדכון מצב המפיקה עם הנתונים שהתקבלו.
            })
            .catch(error => {
                console.error("Error fetching events:", error); // טיפול בשגיאות.
            });
    }, [pMail]); // התלות היא ב-pMail.

    return (
        <div>
            <h4>שם: {producer.name}</h4> 
            <p>{producer.email} : מייל</p> 
            <p>מספר פלאפון: {producer.phone}</p>
            <p>תאור קצר: {producer.shortDescreption}</p> 
        </div>
    );
};
