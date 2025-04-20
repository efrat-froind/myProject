import mongoose from 'mongoose';
const Schema = mongoose.Schema;

mongoose.connect("mongodb://127.0.0.1:27017/EventProduction");

const productiontype= new Schema({
    name: String,
    descreption: String,
    ProducerMail: String
});

const Productiontype = mongoose.model('Productiontype', productiontype);
export default Productiontype; 