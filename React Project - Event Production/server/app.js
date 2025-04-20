import cors from 'cors';
import mongoose from 'mongoose';
import express from 'express'
const app =express();
const PORT = 3001;
// import Productiontype from './Modules/productionTypeModule.js'
// import producer from './Modules/producerModule.js';
import eventController from './controllers/eventController.js'
import eventProducerController from './controllers/eventProducerController.js'


async function startServer() {
    try {
        await mongoose.connect("mongodb://127.0.0.1:27017/EventProduction");
        console.log('Connected To MongoDB');

        app.listen(PORT, () => console.log(`Server running on port ${PORT}`));
    } catch (error) {
        console.error('Error connecting to MongoDB:', error);
    }
}
startServer();

app.use(cors());
app.use(express.json());
app.use('/eventProducerController', eventProducerController)
app.use('/eventController', eventController)