import { useState, useEffect } from "react"; // מייבא useState ו-useEffect מ-React.
import { NavLink } from "react-router-dom"; // מייבא את NavLink מ-React Router.
import { getEvents } from "../api/event.api"; // מייבא פונקציה ל-API להבאת אירועים.

export const User = () => {
    const [events, setEvents] = useState<any[]>([]); // מצב לאחסון רשימת האירועים.
    const [loading, setLoading] = useState<boolean>(true); // מצב לטעינה.
    const [searchEvent, setSearchEvent] = useState(''); // מצב לאחסון טקסט החיפוש.

    // פילטר עבור האירועים על פי טקסט החיפוש.
    const filteredEvents = events.filter(event =>
        event.name.toLowerCase().includes(searchEvent.toLowerCase()) // מסנן אירועים לפי שם האירוע.
    );

    // שימוש ב-useEffect כדי להביא את האירועים מה-API כאשר הרכיב נטען.
    useEffect(() => {
        setLoading(true); // מתחיל את מצב הטעינה.
        getEvents() // קריאה ל-API להבאת האירועים.
            .then(fetchedEvents => {
                setEvents(fetchedEvents); // עדכון מצב האירועים עם האירועים שהתקבלו.
            })
            .catch(error => {
                console.error("Error fetching events:", error); // טיפול בשגיאות.
            })
            .finally(() => {
                setLoading(false); // מסיים את מצב הטעינה.
            });
    }, []); // התלות היא ברשימה ריקה, כלומר זה יקרה רק בפעם הראשונה שהרכיב נטען.

    return (
        <div>
            <button>
                <NavLink to={"/producers"}>כניסת מפיקות</NavLink> {/* קישור למפיקות */}
            </button>
            {loading ? ( // אם עדיין טוען, מציג הודעה מתאימה.
                <p>טוען אירועים...</p>
            ) : (
                filteredEvents.map(event => ( // מציג את האירועים המסוננים.
                    <div key={String(event._id)}> 
                        <NavLink to={`/event-details-for-user/${event._id}`}>
                            {event.name} {/* שם האירוע */}
                        </NavLink>
                    </div>
                ))
            )}
            <input 
                type="text" 
                placeholder='search' 
                value={searchEvent} 
                onChange={(e) => setSearchEvent(e.target.value)} // עדכון מצב החיפוש עם הטקסט שהוזן.
            />
        </div>
    );
};
