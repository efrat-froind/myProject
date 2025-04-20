import { NavLink, useParams } from "react-router-dom"; // מייבא את NavLink ואת useParams.
import { getProducer, updatedProducer } from "../api/producer.api"; // מייבא פונקציות ל-API.
import { useEffect, useState } from "react"; // מייבא useEffect ו-useState מ-React.

export const ProducerDetailsForProducers = () => {
    const { email } = useParams(); // מקבל את המייל מה-URL.
    const [producer, setProducer] = useState<any>(''); // מצב עבור פרטי המפיקה.
    const [isUpdate, setIsUpdate] = useState(false); // מצב עבור מצב העריכה.
    const [name, setName] = useState(''); // מצב עבור שם המפיקה.
    const [Email, setEmail] = useState(''); // מצב עבור מייל המפיקה.
    const [phone, setPhone] = useState(''); // מצב עבור טלפון המפיקה.
    const [shortDescreption, setShortDescreption] = useState(''); // מצב עבור תיאור קצר של המפיקה.

    // שימוש ב-useEffect כדי להביא את פרטי המפיקה מה-API כאשר הרכיב נטען.
    useEffect(() => {
        getProducer(email)
            .then(fetchedProducer => {
                setProducer(fetchedProducer); // עדכון מצב המפיקה עם הנתונים שהתקבלו.
            })
            .catch(error => {
                console.error("Error fetching events:", error); // טיפול בשגיאות.
            });
    }, [email]); // התלות היא ב-email.

    // פונקציה שמטפלת בלחיצה על כפתור העריכה.
    const handleEditClick = () => {
        if (producer) {
            setName(producer.name); // עדכון השם.
            setEmail(producer.email); // עדכון המייל.
            setPhone(producer.phone); // עדכון הטלפון.
            setShortDescreption(producer.shortDescreption); // עדכון התיאור הקצר.
            setIsUpdate(true); // שינוי מצב העריכה ל-true.
        }
    };

    // פונקציה שמטפלת בעדכון המפיקה.
    const updateProducer = async (event: any) => {
        event.preventDefault(); // מונע רענון הדף בעת שליחה.
        try {
            const updatedData = {
                name: name,
                email: Email,
                phone: phone,
                shortDescreption: shortDescreption
            };

            await updatedProducer(updatedData); // קריאה ל-API לעדכון המפיקה.
            setProducer(updatedData); // עדכון מצב המפיקה עם הנתונים החדשים.
            setIsUpdate(false); // שינוי מצב העריכה ל-false.
        } catch (error) {
            console.error("Error updating producer:", error); // טיפול בשגיאות.
        }
    };

    return (
        <div>
            {producer ? (<>
                <p>name: {producer.name}</p>
                <p>email: {producer.email}</p>
                <p>phone: {producer.phone}</p>
                <p>shortDescreption: {producer.shortDescreption}</p>
                <NavLink to={`/producer-event-list/${email}`}>הארועים שלי</NavLink> 
                <br />
                <button onClick={handleEditClick}>עריכה</button> 
                {isUpdate ? ( // אם במצב עריכה, מציג את הטופס.
                    <>
                        <br />
                        <form onSubmit={updateProducer}>
                            <label htmlFor="">name</label>
                            <input type="text" onChange={e => setName(e.target.value)} value={name} /><br />
                            <label htmlFor="">phone</label>
                            <input type="text" onChange={e => setPhone(e.target.value)} value={phone} /><br />
                            <label htmlFor="">shortDescreption</label>
                            <input type="text" onChange={e => setShortDescreption(e.target.value)} value={shortDescreption} /><br />
                            <button type="button" onClick={() => setIsUpdate(false)}>X</button> 
                            <button type="submit">שמירה</button>
                        </form>
                    </>
                ) : (
                <></>
            )}
                </>
            ) : (
                <>
                <p>מפיק זה לא קיים!!!</p>
                <NavLink to={`/personal-area`}>חזור</NavLink>
                </>
            )}
            
        </div>
    );
};
