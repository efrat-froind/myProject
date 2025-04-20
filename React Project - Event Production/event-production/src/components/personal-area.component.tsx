import { useState } from "react"; // מייבא את useState מ-React לניהול מצב.
import { NavLink } from "react-router-dom"; // מייבא פונקציה ליצירת קישורים.

export const PersonalArea = () => {
    const [email, setEmail] = useState(''); // מצב עבור מייל המפיקה.

    return (
        <div>
            <p>כניסה לאיזור האישי</p> 
            <input 
                type="text" 
                placeholder="הכנס מייל" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} // עדכון מצב המייל בעת שינוי.
            /><br />
            <button>
                <NavLink to={`/producer-details-for-producers/${email}`}>שלח</NavLink> 
            </button>
        </div>
    );
};
