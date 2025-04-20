import { useEffect, useState } from "react"; // מייבא את useEffect ו-useState מ-React לניהול מצב ולביצוע פעולות צדדיות.
import { useParams } from "react-router-dom"; // מייבא פונקציה לקבלת פרמטרים מה-URL.
import { getEvent, deleteEvent, updatedEvent } from "../api/event.api"; // מייבא פונקציות לניהול אירועים מה-API.

/**
 * קומפוננטת React המיועדת להציג פרטי אירוע עבור מפיקות, כולל אפשרויות לעריכה ומחיקה.
 */
export const EventDetailsForProducers = () => {
    const { _id } = useParams(); // קבלת ה-ID של האירוע מה-URL.
    const [event, setEvent] = useState<any>(null); // מצב עבור פרטי האירוע.
    const [isUpdate, setIsUpdate] = useState(false); // מצב בוליאני לצורך עריכת האירוע.
    const [name, setName] = useState(''); // מצב עבור שם האירוע.
    const [descreption, setDescreption] = useState(''); // מצב עבור תיאור האירוע.
    const [ProducerMail, setProducerMail] = useState(''); // מצב עבור מייל המפיקה.

    useEffect(() => { // שימוש ב-useEffect כדי להביא את פרטי האירוע בעת טעינת הקומפוננטה.
        getEvent(_id) // קריאה ל-API לקבלת פרטי האירוע.
            .then(fetchedEvents => {
                setEvent(fetchedEvents); // עדכון מצב האירוע עם הנתונים שהתקבלו.
            })
            .catch(error => {
                console.error("Error fetching events:", error); // טיפול בשגיאות.
            });
    }, [_id]); // תלות ב-ID של האירוע.

    /**
     * פונקציה למחיקת האירוע.
     * 
     * @param {any} event - האירוע שברצוננו למחוק.
     */
    const deletedEvent = async (event: any) => {
        try {
            await deleteEvent(event._id); // קריאה ל-API למחיקת האירוע.
            setEvent(null); // עדכון מצב האירוע ל-null לאחר המחיקה.
        } catch (error) {
            console.error("Error deleting event:", error); // טיפול בשגיאות.
        }
    };

    /**
     * פונקציה שמביאה את פרטי האירוע לשדות הקלט לצורך עריכה.
     */
    const handleEditClick = () => {
        if (event) {
            setName(event.name); // עדכון שדה השם עם הנתון מהאירוע.
            setDescreption(event.descreption); // עדכון שדה התיאור עם הנתון מהאירוע.
            setProducerMail(event.ProducerMail); // עדכון שדה המייל עם הנתון מהאירוע.
            setIsUpdate(true); // הגדרת מצב העריכה ל-true.
        }
    };

    /**
     * פונקציה לעדכון האירוע.
     * 
     * @param {Event} event - אובייקט האירוע שנשלח מהטופס.
     */
    const updateEvent = async (event: any) => {
        event.preventDefault(); // מונע רענון הדף בעת שליחה.
        try {
            const updatedData = { // אובייקט עם הנתונים המעודכנים.
                name: name,
                descreption: descreption,
                ProducerMail: ProducerMail,
            };
            
            await updatedEvent(updatedData); // קריאה ל-API לעדכון האירוע.
            setEvent(updatedData); // עדכון מצב האירוע עם הנתונים המעודכנים.
            setIsUpdate(false); // הגדרת מצב העריכה ל-false לאחר העדכון.
        } catch (error) {
            console.error("Error updating event:", error); // טיפול בשגיאות.
        }
    };

    return (
        <div>
            {event ? ( // אם האירוע קיים.
                <>
                    <h4>שם: {event.name}</h4> 
                    <p>תאור הארוע: {event.descreption}</p> 
                    <p>{event.ProducerMail} :מייל מפיקה</p>
                    <button onClick={() => deletedEvent(event)}>מחיקה</button> 
                    <button onClick={handleEditClick}>עריכה</button> 
                    {isUpdate ? ( // אם במצב עריכה.
                        <>
                            <br />
                            <form action="" onSubmit={updateEvent}>
                                <input type="text" onChange={e => setName(e.target.value)} value={name} /><br /> 
                                <input type="text" onChange={e => setDescreption(e.target.value)} value={descreption} /><br /> 
                                <input type="text" onChange={e => setProducerMail(e.target.value)} value={ProducerMail} /><br /> 
                                <button onClick={() => setIsUpdate(false)}>X</button> 
                                <button type="submit">שמירה</button> 
                            </form>
                        </>
                    ) : (
                        <> 
                        </>
                    )}
                </>
            ) : (
                <h4>האירוע נמחק או לא נמצא</h4> // הודעה במקרה שהאירוע אינו קיים.
            )}
        </div>
    );
};
