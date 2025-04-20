import { useState } from "react"; // מייבא את useState מ-React לניהול מצב.
import { NavLink, useParams } from "react-router-dom"; // מייבא פונקציות ליצירת קישורים ולקבלת פרמטרים מה-URL.
import { addEvent } from "../api/event.api"; // מייבא פונקציה להוספת אירוע לשרת.

/**
 * קומפוננטת React המאפשרת למפיקות להוסיף אירועים.
 * מנהלת את מצב השדות (שם ותיאור) ומבצעת קריאה לשרת.
 */
export const AddEvent = () => {
    const [name, setName] = useState(''); // מצב עבור שם האירוע.
    const [descreption, setDescreption] = useState(''); // מצב עבור תיאור האירוע.
    const { email } = useParams(); // קבלת ה-email מה-URL.
    const [isAdded, setIsAdded] = useState(false); // מצב בוליאני עבור הצלחת ההוספה.

    /**
     * פונקציה להוספת אירוע.
     * 
     * @param {Event} e - אובייקט האירוע מהטופס.
     */
    const add_event = async (e: any) => {
        e.preventDefault(); // מונע רענון הדף בעת שליחה.
        try {
            const newEvent = { // אובייקט חדש לאירוע.
                name: name, // שם האירוע מה-state.
                descreption: descreption, // תיאור האירוע מה-state.
                ProducerMail: email // ה-email של המפיקה.
            };
            await addEvent(newEvent); // קריאה לשרת להוספת האירוע.
            setIsAdded(true); // עדכון מצב ההצלחה.
            setName(''); // ניקוי שדה השם.
            setDescreption(''); // ניקוי שדה התיאור.
        } catch (error) { // טיפול בשגיאות.
            console.log("", error); // רישום השגיאה לקונסול.
            setIsAdded(false); // עדכון מצב הכישלון.
        }
    };

    return ( // JSX של הקומפוננטה.
        <div>
            <form action="" onSubmit={add_event}> 
                <label htmlFor="">name</label> 
                <input required type="text" value={name} onChange={e => setName(e.target.value)} /><br /> 
                <label htmlFor="">descreption</label> 
                <input required type="text" value={descreption} onChange={e => setDescreption(e.target.value)} /><br />
                <button type="submit"> הוסף</button> 
            </form>
            {isAdded ? ( // בדיקה אם האירוע נוסף בהצלחה.
                <div>
                    <p>הנתונים נקלטו בהצלחה!!</p> 
                    <NavLink to={`/producer-event-list/${email}`}>חזור</NavLink>
                </div>
            ) : (
                <> 
                </>
            )}
        </div>
    );
};
