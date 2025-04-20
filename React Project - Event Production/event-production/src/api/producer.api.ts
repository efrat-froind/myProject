import axios from "axios"; // מייבא את axios לביצוע קריאות HTTP.
import { Producer } from "../types/producer"; // מייבא את סוג המפיק.

const server = 'http://localhost:3001'; // כתובת השרת.
const producerServer = axios.create({
    baseURL: server // יוצר מופע של axios עם כתובת בסיסית.
});

// פונקציה להוספת מפיק חדש.
export const newProducer = async (producer: Producer) => {
    const response = await producerServer.post(`/eventProducerController/postEventProducer`, producer); // מבצע קריאה להוספת מפיק.
    return response; // מחזיר את התגובה מהשרת.
};

// פונקציה לקבלת מפיק לפי דוא"ל.
export const getProducer = async (email: any): Promise<Producer> => {
    const response = await producerServer.get(`/eventProducerController/getEventProducer/${email}`); // מבצע קריאה ל-API לקבלת המפיק.
    return response.data; // מחזיר את הנתונים של המפיק.
};

// פונקציה לעדכון פרטי מפיק קיים.
export const updatedProducer = async (producer: any) => {
    const response = await producerServer.put(`/eventProducerController/putEventProducer`, producer); // מבצע קריאה לעדכון המפיק.
    return response.data; // מחזיר את הנתונים המעודכנים.
};
