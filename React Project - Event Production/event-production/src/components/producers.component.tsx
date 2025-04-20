import { NavLink } from "react-router-dom"; // מייבא את NavLink מ-React Router.

export const Producers = () => {
    return (
        <div>
            <button>
                <NavLink to={`/new-producer`}>מפיקה חדשה</NavLink> {/* קישור ליצירת מפיקה חדשה */}
            </button>
            <br /><br />
            <button>
                <NavLink to={'/personal-area'}>כניסה</NavLink> {/* קישור לאזור האישי */}
            </button>
        </div>
    );
};
