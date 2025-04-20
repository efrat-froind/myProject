import axios from "axios"; // מייבא את axios לביצוע קריאות HTTP.
import { Event } from "../types/event"; // מייבא את סוג האירוע.

const server = 'http://localhost:3001'; // כתובת השרת.
const EventProductionServer = axios.create({
    baseURL: server // יוצר מופע של axios עם כתובת בסיסית.
});

// פונקציה לקבלת כל האירועים.
export const getEvents = async (): Promise<Event[]> => {
    const response = await EventProductionServer.get<Event[]>('/eventController/api/getEvents'); // מבצע קריאה ל-API.
    return response.data; // מחזיר את הנתונים.
};

// פונקציה לקבלת אירוע לפי מזהה.
export const getEvent = async (_id: any): Promise<Event> => {
    const response = await EventProductionServer.get<Event>(`/eventController/api/getEvent/${_id}`); // מבצע קריאה ל-API עם מזהה האירוע.
    return response.data; // מחזיר את הנתונים של האירוע.
};

// פונקציה למחיקת אירוע לפי מזהה.
export const deleteEvent = async (_id: any) => {
    const response = await EventProductionServer.delete(`/eventController/deleteEvent/${_id}`); // מבצע קריאה למחיקת האירוע.
    return response.data; // מחזיר את הנתונים (אם יש).
};

// פונקציה לעדכון אירוע.
export const updatedEvent = async (event: any) => {
    const response = await EventProductionServer.put(`/eventController/putEvent`, event); // מבצע קריאה לעדכון האירוע.
    return response.data; // מחזיר את הנתונים המעודכנים.
};

// פונקציה להוספת אירוע חדש.
export const addEvent = async (event: any) => {
    const response = await EventProductionServer.post(`/eventController/postEvent`, event); // מבצע קריאה להוספת האירוע.
    console.log(response.data + " in addEvent"); // מדפיס את הנתונים שהתקבלו.
    return response.data; // מחזיר את הנתונים של האירוע החדש.
};

