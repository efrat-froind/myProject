import { useEffect, useState } from "react"; // מייבא את useEffect ו-useState מ-React לניהול מצב ולביצוע פעולות צדדיות.
import { NavLink, useParams } from "react-router-dom"; // מייבא פונקציות לקבלת פרמטרים מה-URL וליצירת קישורים.
import { getEvent } from "../api/event.api"; // מייבא פונקציה לקבלת פרטי האירוע מה-API.

/**
 * קומפוננטת React המיועדת להציג פרטי אירוע עבור משתמשים, כולל פרטי המפיקה.
 */
export const EventDetailsForUser = () => {
    const { _id } = useParams(); // קבלת ה-ID של האירוע מה-URL.
    const [event, setEvent] = useState<any>([]); // מצב עבור פרטי האירוע.
    const [pMail, setPMail] = useState(''); // מצב עבור מייל המפיקה.

    useEffect(() => { // שימוש ב-useEffect כדי להביא את פרטי האירוע בעת טעינת הקומפוננטה.
        getEvent(_id) // קריאה ל-API לקבלת פרטי האירוע.
            .then(fetchedEvents => {
                setEvent(fetchedEvents); // עדכון מצב האירוע עם הנתונים שהתקבלו.
                setPMail(String(fetchedEvents.ProducerMail)); // עדכון מצב המייל עם המייל של המפיקה.
            })
            .catch(error => {
                console.error("Error fetching events:", error); // טיפול בשגיאות.
            });
    }, []); // תלות ריקה, כלומר הקומפוננטה תתעדכן רק בעת הטעינה הראשונית.

    return (
        <div>
            <p>שם ארוע: {event.name}</p>
            <p>תאור: {event.descreption}</p> 
            <p>:פרטי מפיקה</p> 
            <NavLink to={`/producer-details-for-users/${pMail}`}>{event.ProducerMail}</NavLink> 
        </div>
    );
};
