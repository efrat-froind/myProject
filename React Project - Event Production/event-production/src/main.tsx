// ייבוא רכיב StrictMode מ-React, עוזר לזהות בעיות פוטנציאליות בקוד
import { StrictMode } from 'react'

// ייבוא הפונקציה createRoot מ-react-dom/client, מאפשרת ליצור שורש ליישום React
import { createRoot } from 'react-dom/client'

// ייבוא רכיבי BrowserRouter, Route, ו-Routes מ-react-router-dom לניהול מסלולים
import { BrowserRouter, Route, Routes } from 'react-router-dom'; // ייבוא Router

// ייבוא הקומפוננטה הראשית של היישום
import App from './App'; 

// ייבוא קובץ CSS עבור עיצוב היישום
import './index.css'

// ייבוא קומפוננטות שונות מהקבצים שלך
import { EventDetailsForUser } from './components/event-details-for-user.component';
import { Producers } from './components/producers.component';
import { NewProducer } from './components/new-producer.component';
import { PersonalArea } from './components/personal-area.component';
import { ProducerDetailsForProducers } from './components/producer-details-for-producers.component';
import { ProducerDetailsForUsers } from './components/producer-details-for-users.component';
import { EventDetailsForProducers } from './components/event-details-for-producers.component';
import { ProducerEventList } from './components/producer-event-list.component';
import { AddEvent } from './components/add-event.component';

// יצירת שורש ליישום והצגת התוכן ב-DOM
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    {/* עטיפת הקומפוננטה ב-Router לניהול ניווט */}
    <BrowserRouter> 
        <Routes>
          {/* הגדרת המסלול הראשי */}
          <Route path="/" element={<App/>}/>
          {/* מסלול לפרטי אירוע עבור משתמש */}
          <Route path='/event-details-for-user/:_id' element={<EventDetailsForUser/>}/>
          {/* מסלול לרשימת המפיקים */}
          <Route path="/producers" element={<Producers/>}/>
          {/* מסלול ליצירת מפיק חדש */}
          <Route path='/new-producer' element={<NewProducer/>}/>
          {/* מסלול לאזור אישי של המשתמש */}
          <Route path='/personal-area' element={<PersonalArea/>}/>
          {/* מסלול לפרטי מפיק עבור מפיקים */}
          <Route path='/producer-details-for-producers/:email' element={<ProducerDetailsForProducers/>}/>
          {/* מסלול לפרטי מפיק עבור משתמשים */}
          <Route path='/producer-details-for-users/:pMail' element={<ProducerDetailsForUsers/>}/>
          {/* מסלול לפרטי אירוע עבור מפיקים */}
          <Route path='/event-details-for-producers/:_id' element={<EventDetailsForProducers/>}/>
          {/* מסלול לרשימת אירועים של מפיק */}
          <Route path='/producer-event-list/:email' element={<ProducerEventList/>}/>
          {/* מסלול לפרטי אירוע ברשימת אירועים של מפיק */}
          <Route path='/producer-event-list/:email/event-details-for-producers/:_id' element={<EventDetailsForProducers />} />
          {/* מסלול להוספת אירוע */}
          <Route path='/add-event/:email' element={<AddEvent/>}/>
        </Routes>
    </BrowserRouter>
  </StrictMode>,
)
