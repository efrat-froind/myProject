import { useState } from "react"; // מייבא את useState מ-React לניהול מצב.
import { newProducer } from "../api/producer.api"; // מייבא פונקציה ליצירת מפיקה מה-API.
import { NavLink } from "react-router-dom"; // מייבא פונקציה ליצירת קישורים.

export const NewProducer = () => {
    // הגדרת מצבים עבור השדות השונים.
    const [name, setName] = useState(''); // מצב עבור שם המפיקה.
    const [email, setEmail] = useState(''); // מצב עבור מייל המפיקה.
    const [phone, setPhone] = useState(''); // מצב עבור טלפון המפיקה.
    const [shortDescreption, setShortDescreption] = useState(''); // מצב עבור תיאור קצר של המפיקה.
    const [isSuccees, setIsSuccees] = useState(''); // מצב עבור הצלחת הפעולה.

    /**
     * פונקציה שמטפלת בשליחת הטופס.
     * 
     * @param {any} e - האירוע של שליחת הטופס.
     */
    const handleSubmit = async (e: any) => {
        // בדיקה אם כל השדות מלאים.
        if (!name || !email || !phone || !shortDescreption) {
            alert("חובה למלא את כל השדות!!"); // הצגת הודעת שגיאה אם יש שדות ריקים.
            return; 
        }

        e.preventDefault(); // מונע רענון הדף בעת שליחה.
        const producerData = { // אובייקט עם הנתונים של המפיקה.
            name,
            email,
            phone,
            shortDescreption
        };

        try {
            const response = await newProducer(producerData); // קריאה ל-API ליצירת מפיקה חדשה.
            console.log(response.data); // הדפסת התגובה מהשרת.
            setIsSuccees("true"); // עדכון מצב ההצלחה ל-true.
        } catch (error) {
            console.log("error creating producer:", error); // טיפול בשגיאות.
            setIsSuccees("false"); // עדכון מצב ההצלחה ל-false.
        }
    };

    return (
        <div>
            <p>יצירת מפיקה</p> 
            <form onSubmit={handleSubmit}> 
                <input type="text" placeholder="name producer" value={name} onChange={(e) => setName(e.target.value)} /><br />
                <input type="text" placeholder="email producer" value={email} onChange={(e) => setEmail(e.target.value)} /><br />
                <input type="text" placeholder="phone producer" value={phone} onChange={(e) => setPhone(e.target.value)} /><br />
                <input type="text" placeholder="shortDescreption producer" value={shortDescreption} onChange={(e) => setShortDescreption(e.target.value)} /><br />
                <button type="submit">שלח</button><br />
            </form>
            {isSuccees === "true" ? ( // אם הפעולה הצליחה.
                <div>
                    <p>הנתונים נקלטו בהצלחה!!</p> 
                    <button><NavLink to={'/personal-area'}>כניסה לאיזור האישי</NavLink></button> 
                </div>
            ) : isSuccees === "false" ? ( // אם הייתה שגיאה.
                <p>שגיאה. אנא נסה שוב!</p>
            ) : null}
        </div>
    );
};
