import { useEffect, useState } from "react"; // מייבא useEffect ו-useState מ-React.
import { getEvents } from "../api/event.api"; // מייבא פונקציה ל-API להבאת אירועים.
import { NavLink, useParams } from "react-router-dom"; // מייבא NavLink ו-useParams מ-React Router.

export const ProducerEventList = () => {
    const [events, setEvents] = useState<any[]>([]); // מצב עבור רשימת האירועים.
    const { email } = useParams<{ email: string }>(); // מקבל את המייל מה-URL.

    // שימוש ב-useEffect כדי להביא את האירועים כאשר המייל קיים.
    useEffect(() => {
        if (email) { 
            localStorage.setItem('producerEmail', email); // שומר את המייל ב-localStorage.
            getEvents() // קריאה ל-API להבאת האירועים.
                .then(fetchedEvents => {
                    const filteredEvents = fetchedEvents.filter(event => event.ProducerMail === email); // מסנן אירועים לפי המייל.
                    setEvents(filteredEvents); // עדכון מצב האירועים עם האירועים המסוננים.
                })
                .catch(error => {
                    console.log("Error fetching events:", error); // טיפול בשגיאות.
                });
        } else {
            console.log("Email is undefined"); // טיפול במקרה שאין מייל.
        }
    }, [email]); // התלות היא ב-email.

    // פונקציה לעדכון האירועים לפי המייל שנשמר ב-localStorage.
    const updateEventsByEmail = () => {
        const storedEmail = localStorage.getItem('producerEmail'); // מקבל את המייל הנשמר.
        if (storedEmail) {
            getEvents() // קריאה ל-API להבאת האירועים.
                .then(fetchedEvents => {
                    const filteredEvents = fetchedEvents.filter(event => event.ProducerMail === storedEmail); // מסנן אירועים לפי המייל.
                    setEvents(filteredEvents); // עדכון מצב האירועים עם האירועים המסוננים.
                })
                .catch(error => {
                    console.log("Error fetching events:", error); // טיפול בשגיאות.
                });
        }
    };

    // שימוש ב-useEffect כדי לעדכן את האירועים כאשר הרכיב נטען.
    useEffect(() => {
        updateEventsByEmail(); // קריאה לפונקציה לעדכון האירועים.
    }, []); 

    return (
        <div>
            {events.length > 0 ? ( // אם יש אירועים, מציג אותם.
                <>
                    <h2>אירועים עבור: <br />{email}</h2>
                    <ul>
                        {events.map(event => (
                            <li key={event._id}> 
                                <NavLink to={`event-details-for-producers/${event._id}`}>{event.name}<br /><br /></NavLink>
                            </li>
                        ))}
                    </ul>
                    <button><NavLink to={`/add-event/${email}`}>הוספת ארוע</NavLink></button>
                </>
            ) : ( // אם אין אירועים, מציג הודעה מתאימה.
                <>
                    <h2>!!אין ארועים {email} ל </h2>
                    <button><NavLink to={`/add-event/${email}`}>הוספת ארוע</NavLink></button>
                </>
            )}
        </div>
    );
};
